namespace LibraryAPI.Domain.Entities.Auth
{
    public class User
    {
        public int Id { get; set; }

        public int OrganisationId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public Role Role { get; set; }

        public string? ResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}