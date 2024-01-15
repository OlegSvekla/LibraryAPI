using LibraryApi.BL.Validators.IValidators;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LibraryAPI.BL.Validators
{
    public class NameValidator : INameValidator
    {
        private const int MaxFirstNameLength = 40;
        private const int MaxLastNameLength = 40;
        private const int MaxFullNameLength = 120;

        private static readonly Regex FirstNameRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁіІўЎ’'.\-,]+$");
        private static readonly Regex LastNameRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁіІўЎ’'.\-,]+$");
        private static readonly Regex FullNameRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁіІўЎ’'.\-, ]+$");

        public NameValidator() { }

        public void ValidateFirstName(string? firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ValidationException("FirstName is required");

            if (firstName.Length > MaxFirstNameLength)
                throw new ValidationException($"FirstName must be less than {MaxFirstNameLength} characters");

            if (!FirstNameRegex.IsMatch(firstName))
                throw new ValidationException("Incorrect First Name Entered");
        }

        public void ValidateLastName(string? lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ValidationException("LastName Is Required");

            if (lastName.Length > MaxLastNameLength)
                throw new ValidationException($"LastName must be less than {MaxLastNameLength} characters");

            if (!LastNameRegex.IsMatch(lastName))
                throw new ValidationException("Incorrect LastName entered");
        }

        public void ValidateFullName(string? fullname)
        {
            if (string.IsNullOrWhiteSpace(fullname))
                throw new ValidationException("FullName is required");

            if (fullname.Length > MaxFullNameLength)
                throw new ValidationException($"FullName must be less than {MaxFullNameLength} characters");

            if (!FullNameRegex.IsMatch(fullname))
                throw new ValidationException("Incorrect FullName entered");
        }
    }
}