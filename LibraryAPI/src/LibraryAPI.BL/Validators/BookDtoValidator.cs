using LibraryApi.BL.Validators.IValidators;
using LibraryAPI.BL.DTOs;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.BL.Validators
{
    public class BookDtoValidator : IBookDtoValidator
    {
        private const int MaxTitleLength = 20;
        private const int MaxIsbnLength = 20;
        private const int MaxGenreLength = 15;
        private const int MaxDescriptionLength = 255;

        public void Validate(BookDto model)
        {
            ValidateTitle(model.Title);
            ValidateIsbn(model.Isbn);
            ValidateGenre(model.Genre);
            ValidateDescription(model.Description);
            ValidateBorrowedReturnDate(model.BorrowedDate, model.ReturnDate);
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ValidationException($"The Title is required");

            if (title.Length > MaxTitleLength)
                throw new ValidationException($"The Title must be no more than {MaxTitleLength} characters");
        }

        private void ValidateIsbn(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                throw new ValidationException($"The Isbn is required");

            if (isbn.Length > MaxIsbnLength)
                throw new ValidationException($"The Isbn must be no more than {MaxIsbnLength} characters");
        }

        private void ValidateGenre(string genre)
        {
            if (string.IsNullOrEmpty(genre))
                throw new ValidationException($"The Genre is required");

            if (genre.Length > MaxIsbnLength)
                throw new ValidationException($"The Genre must be no more than {MaxGenreLength} characters");
        }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ValidationException($"The escription is required");

            if (description.Length > MaxIsbnLength)
                throw new ValidationException($"The Description must be no more than {MaxDescriptionLength} characters");
        }

        private void ValidateBorrowedReturnDate(DateTime borrowedDate, DateTime returnDate)
        {
            if (borrowedDate == default || borrowedDate == null)
                throw new ValidationException("Borrowed date is required.");

            if (returnDate == default || returnDate == null)
                throw new ValidationException("Return date is required.");

            if (returnDate < borrowedDate)
                throw new ValidationException("Return date must be after or equal to borrowed date.");
        }
    }
}