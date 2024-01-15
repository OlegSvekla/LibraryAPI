using LibraryAPI.BL.DTOs;

namespace LibraryApi.BL.Validators.IValidators
{
    public interface IAuthorDtoValidator
    {
        void Validate(AuthorDto authorDto);
    }
}