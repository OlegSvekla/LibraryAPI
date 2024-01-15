using AutoMapper;
using LibraryApi.BL.Validators.IValidators;
using LibraryAPI.BL.Helpers;
using LibraryAPI.Domain.Constants;
using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Interfaces.IRepositories;
using LibraryAPI.Domain.Interfaces.IServices;
using LibraryAPI.Domain.Requests;
using LibraryAPI.Domain.Responses;
using LibraryAPI.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace LibraryAPI.BL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        private readonly IJwtUtils jwtUtils;
        private readonly IMapper mapper;
        private readonly AuthSettings appSettings;
        private readonly IAuthRequestValidator authRequestValidator;
        private readonly ILogger<AccountService> logger;

        public AccountService(

            IJwtUtils jwtUtils,
            IMapper mapper,
            IUserRepository userRepository,
            IOptions<AuthSettings> appSettings,
            IEmailService emailService,
            IAuthRequestValidator authRequestValidator,
            ILogger<AccountService> logger)
        {
            this.jwtUtils = jwtUtils;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.appSettings = appSettings.Value;
            this.emailService = emailService;
            this.authRequestValidator = authRequestValidator;
            this.logger = logger;
        }

        public async Task<UserDetailsResponse> Authenticate(AuthRegisterRequest model, string? ipAddress)
        {
            authRequestValidator.Validate(model);

            var user = await userRepository.GetOneByAsync(expression: _ => _.Email.Equals(model.Email),
                cancellationToken: CancellationToken.None);

            ValidateUser(user, model.Password);

            var refreshToken = jwtUtils.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(refreshToken);
            RemoveOldRefreshTokens(user);

            user.LastAccessedOn = DateTime.UtcNow;

            await userRepository.UpdateAsync(user);

            var response = mapper.Map<UserDetailsResponse>(user);
            response.RefreshToken = refreshToken.Token;
            response.JwtToken = jwtUtils.GenerateJwtToken(user);

            return response;
        }

        public async Task<User> RegisterAsync<T>(T model, Role role) where T : AuthRegisterRequest
        {
            authRequestValidator.Validate(model);

            var user = await userRepository.GetOneByAsync(expression: _ => _.Email.Equals(model.Email),
                cancellationToken: CancellationToken.None);
            if (user is not null)
            {
                logger.LogInformation("User is already existing. There is ValidationException.");
                throw new ValidationException("User is already existing");
            }

            var account = mapper.Map<User>(model);

            account.Role = role;
            account.VerificationToken = await GenerateVerificationToken();
            account.PasswordHash = PasswordHasher.HashPassword(model.Password);

            var created = await userRepository.CreateAsync(account, cancellationToken: CancellationToken.None);

            emailService.Send(created, subject: Constants.Email.Subject);

            return created;
        }

        public async Task<User> VerifyEmailAsync(string token)
        {
            if (token is null)
            {
                logger.LogInformation("VerificationToken cannot be null. There is ArgumentNullException.");
                throw new ArgumentNullException();
            }

            var account = await userRepository.GetOneByAsync(expression: _ => _.VerificationToken.Equals(token),
                cancellationToken: CancellationToken.None);

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;
            account.IsVerified = true;

            await userRepository.UpdateAsync(account);

            return account;
        }

        private async Task<string> GenerateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var userWithAlreadyExistingToken = await userRepository.GetOneByAsync(expression: _ => _.VerificationToken == token,
                cancellationToken: CancellationToken.None);
            if (userWithAlreadyExistingToken is not null)
            {
                return await GenerateVerificationToken();
            }

            return token;
        }

        private void RemoveOldRefreshTokens(User account)
        {
            account.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private User ValidateUser(User? user, string password)
        {
            if (user is null)
            {
                logger.LogInformation($"User not found Exception.");
                throw new NotFoundException("User Not Found");
            }

            if (!user.IsVerified)
            {
                logger.LogInformation("Email Not Verified. There is AppException.");
                throw new AppException("Email Not Verified");
            }

            if (!PasswordHasher.Verify(password, user.PasswordHash))
            {
                logger.LogInformation("Password Incorrect. There is AppException.");
                throw new AppException("Password Incorrect");
            }

            return user;
        }
    }
}