using AutoMapper;
using LibraryAPI.BL.Helpers;
using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Interfaces.IRepository;
using LibraryAPI.Domain.Interfaces.IService;
using LibraryAPI.Domain.Request;
using LibraryAPI.Domain.Response;
using LibraryAPI.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace LibraryAPI.BL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtUtils jwtUtils;
        private readonly IMapper mapper;
        private readonly AuthSettings appSettings;
        private readonly IEmailService emailService;

        public AccountService(

            IJwtUtils jwtUtils,
            IMapper mapper,
            IUserRepository userRepository,
            IOptions<AuthSettings> appSettings,
            IEmailService emailService)
        {
            //this.context = context;
            this.jwtUtils = jwtUtils;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.appSettings = appSettings.Value;
            this.emailService = emailService;
        }

        public async Task<UserDetailsResponse> Authenticate(BaseAuhtRequest model, string? ipAddress)
        {
            var user = await userRepository.GetOneByAsync(expression: _ => _.Email.Equals(model.Email));

            //user = ValidateUser(user, model.Password);

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

        public async Task<User> RegisterAsync<T>(T model, Role role) where T : RegisterRequest
        {
            //accountValidator.Validate(model); напиши валидатор

            var user = await userRepository.GetOneByAsync(expression: _ => _.Id.Equals(model.Email));
            if (user is not null) throw new ValidationException("User is already existing");

            var account = mapper.Map<User>(model);
            account.Role = role;
            account.VerificationToken = GenerateVerificationToken();
            account.PasswordHash = PasswordHasher.HashPassword(model.Password);

            var created = await userRepository.CreateAsync(account);

            emailService.Send(created, subject: $"FromLibraryApi");

            return created;
        }

        public async Task<User> VerifyEmailAsync(string token)
        {
            ArgumentNullException.ThrowIfNull(token);

            var account = await userRepository.GetOneByAsync(expression: _ => _.VerificationToken.Equals(token));

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            await userRepository.UpdateAsync(account);

            return account;
        }

        private string GenerateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var existingtoken = userRepository.GetOneByAsync(expression: _ => _.VerificationToken == token);
            if (existingtoken == null)
                return GenerateVerificationToken();

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
                throw new NotFoundException("UserNotFound");

            if (!user.IsVerified)
                throw new AppException("EmailNotVerified");

            if (!PasswordHasher.Verify(password, user.PasswordHash))
                throw new AppException("EmailOrPasswordIncorrect");

            return user;
        }
    }
}