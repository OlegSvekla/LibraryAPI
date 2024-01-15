using System.Text.Json.Serialization;

namespace LibraryAPI.Domain.Responses
{
    public class UserDetailsResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsEmailVerified { get; set; }

        public string JwtToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}