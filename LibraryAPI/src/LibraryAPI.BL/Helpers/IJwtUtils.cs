using LibraryAPI.Domain.Entities.Auth;

namespace LibraryAPI.BL.Helpers
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User account);

        public int? ValidateJwtToken(string? token);

        public RefreshToken GenerateRefreshToken(string? ipAddress);
    }
}