using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Domain.Entities.Auth
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Token { get; set; }
        public string? CreatedByIp { get; set; }

        public DateTime? Expires { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;
    }
}