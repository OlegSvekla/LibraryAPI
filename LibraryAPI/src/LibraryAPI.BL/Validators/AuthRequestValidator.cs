using LibraryApi.BL.Validators.IValidators;
using LibraryAPI.Domain.Requests;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LibraryAPI.BL.Validators
{
    public class AuthRequestValidator : IAuthRequestValidator
    {
        private const int MinPasswordLength = 8;
        private const int MaxPasswordLength = 64;

        private const int MaxEmailLength = 120;

        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public AuthRequestValidator() { }

        public void Validate(AuthRegisterRequest model)
        {
            ValidatePassword(model.Password);
            ValidateEmail(model.Email);
        }

        public void ValidatePassword(string password)
        {
            if (password.Length < MinPasswordLength)
                throw new ValidationException($"The Password must be at least {MinPasswordLength} characters");

            if (password.Length > MaxPasswordLength)
                throw new ValidationException($"The Password must be no more than {MaxPasswordLength} characters");
        }

        public void ValidateEmail(string email)
        {
            if (!EmailRegex.IsMatch(email))
                throw new ValidationException("Incorrect Email Entered");

            if (email.Length > MaxEmailLength)
                throw new ValidationException($"Email must be no more than {MaxEmailLength} characters");
        }
    }
}