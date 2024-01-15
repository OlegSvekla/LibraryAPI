namespace LibraryAPI.Domain.Requests
{
    public class AuthRegisterRequest
    {
        private string email;
        public string Email
        {
            get => email;
            set => email = value.Trim().ToLower();
        }

        public string Password { get; set; }
    }
}