using System.Text.Json.Serialization;

namespace LibraryAPI.Domain.Response
{
    public class UserDetailsResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsEmailVerified { get; set; }

        public string JwtToken { get; set; }

        public string? Message { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
