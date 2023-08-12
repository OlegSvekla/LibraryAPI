using FluentValidation;
using LibraryAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Core.Validation
{
    public sealed class BookDtoValidator : AbstractValidator<BookDto>
    {
        //TODO REWRITE
        public BookDtoValidator()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(book => book.Isbn)
                .NotEmpty().WithMessage("ISBN is required.")
                .MaximumLength(20).WithMessage("ISBN must not exceed 20 characters.");

            RuleFor(book => book.Genre)
                .NotEmpty().WithMessage("Genre is required.")
                .MaximumLength(50).WithMessage("Genre must not exceed 50 characters.");

            RuleFor(book => book.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(book => book.BorrowedDate)
                .NotEmpty().WithMessage("Borrowed date is required.")
                .LessThanOrEqualTo(book => book.ReturnDate).WithMessage("Borrowed date must be before or equal to return date.");

            RuleFor(book => book.ReturnDate)
                .NotEmpty().WithMessage("Return date is required.")
                .GreaterThanOrEqualTo(book => book.BorrowedDate).WithMessage("Return date must be after or equal to borrowed date.");

            RuleFor(book => book.Author)
                .NotNull().WithMessage("Author is required.");
        }
    }
}