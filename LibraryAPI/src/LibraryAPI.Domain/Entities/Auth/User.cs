using TaskTracker.Domain.Entities;

namespace LibraryAPI.Domain.Entities.Auth
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string? VerificationToken { get; set; }

        public Role Role { get; set; }

        public DateTime? LastAccessedOn { get; set; }

        public DateTime? Verified { get; set; }

        public bool IsVerified { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}