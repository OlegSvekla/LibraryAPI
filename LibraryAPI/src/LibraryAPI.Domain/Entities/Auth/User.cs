using TaskTracker.Domain.Entities;

namespace LibraryAPI.Domain.Entities.Auth
{
    public class User : BaseEntity
    {
        public int OrganisationId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public Role Role { get; set; }

        public string? ResetToken { get; set; }

        public string? VerificationToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public DateTime? LastAccessedOn { get; set; }

        public DateTime? Verified { get; set; }

        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;

        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}