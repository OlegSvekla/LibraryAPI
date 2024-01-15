using LibraryApi.BL.Validators.IValidators;
using LibraryAPI.BL.DTOs;

namespace LibraryAPI.BL.Validators
{
    public class AuthorDtoValidator : IAuthorDtoValidator
    {
        private readonly INameValidator nameValidator;

        public AuthorDtoValidator(INameValidator nameValidator)
        {
            this.nameValidator = nameValidator;
        }

        public void Validate(AuthorDto model)
        {
            nameValidator.ValidateFirstName(model.FirstName);
            nameValidator.ValidateLastName(model.LastName);
            nameValidator.ValidateFullName(model.FullName);
        }
    }
}