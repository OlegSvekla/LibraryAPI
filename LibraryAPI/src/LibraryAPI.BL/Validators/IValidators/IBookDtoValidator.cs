using LibraryAPI.BL.DTOs;

namespace LibraryApi.BL.Validators.IValidators
{
    public interface IBookDtoValidator
    {
        void Validate(BookDto authorDto);
    }
}