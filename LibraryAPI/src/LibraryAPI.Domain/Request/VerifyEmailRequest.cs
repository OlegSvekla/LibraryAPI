using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Domain.Request
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}