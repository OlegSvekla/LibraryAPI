using LibraryAPI.Domain.Constants;
using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Interfaces.IRepository;
using LibraryAPI.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryAPI.Helpers
{
    public class JwtUtils : IJwtUtils
    {
        private const int LifeTimeTokenMinutes = 60 * 4;

        private readonly IUserRepository userRepository;
        private readonly AuthSettings appSettings;

        public JwtUtils(
            IUserRepository userRepository,
            IOptions<AuthSettings> appSettings)
        {
            this.userRepository = userRepository;
            this.appSettings = appSettings.Value;
        }

        public string GenerateJwtToken(User account)
        {
            //TODO: remove after testing

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(Constants.Claims.UserId, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: appSettings.Issuer,
                audience: appSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(LifeTimeTokenMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int? ValidateJwtToken(string? token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = appSettings.Issuer,
                    ValidAudience = appSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret)),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == Constants.Claims.UserId).Value);

                return accountId;
            }
            catch
            {
                return null;
            }
        }

        public RefreshToken GenerateRefreshToken(string? ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                // token is a cryptographically strong random sequence of values
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                // token is valid for 7 days
                CreatedByIp = ipAddress
            };

            //// ensure token is unique by checking against db
            //var tokenIsUnique = userRepository.!Users.Any(a => a.RefreshTokens.Any(t => t.Token == refreshToken.Token));

            //if (!tokenIsUnique)
            //    return GenerateRefreshToken(ipAddress);

            return refreshToken;
        }

    }
}
