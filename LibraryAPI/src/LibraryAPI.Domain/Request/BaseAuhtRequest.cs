using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Domain.Request
{
    public class BaseAuhtRequest
    {
        private string email;

        [Required]
        public string Email
        {
            get => email;
            set => email = value.Trim().ToLower();
        }

        [Required]
        public string Password { get; set; }
    }
}
