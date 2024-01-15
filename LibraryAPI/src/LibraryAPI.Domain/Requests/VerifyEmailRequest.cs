using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Domain.Requests
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}