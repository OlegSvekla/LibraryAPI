using LibraryAPI.Domain.Requests;

namespace LibraryApi.BL.Validators.IValidators
{
    public interface IAuthRequestValidator
    {
        void Validate(AuthRegisterRequest model);

        void ValidatePassword(string password);

        void ValidateEmail(string email);
    }
}