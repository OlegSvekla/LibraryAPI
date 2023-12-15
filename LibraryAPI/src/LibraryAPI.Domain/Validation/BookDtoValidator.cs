using FluentValidation;
using LibraryAPI.Domain.DTOs;

namespace LibraryAPI.Domain.Validation
{
    public sealed class BookDtoValidator : AbstractValidator<BookDto>
    {
        public BookDtoValidator()
        {
            RuleFor(book => book.Title).NotEmpty()
                                       .WithMessage("Title is required.")
                                       .MaximumLength(15)
                                       .WithMessage("Title must not exceed 15 characters.");

            RuleFor(book => book.Isbn).NotEmpty()
                                      .WithMessage("ISBN is required.")
                                      .MaximumLength(20)
                                      .WithMessage("ISBN must not exceed 20 characters.");

            RuleFor(book => book.Genre).NotEmpty()
                                       .WithMessage("Genre is required.")
                                       .MaximumLength(10)
                                       .WithMessage("Genre must not exceed 10 characters.");

            RuleFor(book => book.Description).NotEmpty()
                                             .WithMessage("Description is required.");

            RuleFor(book => book.BorrowedDate).NotEmpty()
                                              .WithMessage("Borrowed date is required.")
                                              .LessThanOrEqualTo(book => book.ReturnDate)
                                              .WithMessage("Borrowed date must be before or equal to return date.");

            RuleFor(book => book.ReturnDate).NotEmpty()
                                            .WithMessage("Return date is required.")
                                            .GreaterThanOrEqualTo(book => book.BorrowedDate)
                                            .WithMessage("Return date must be after or equal to borrowed date.");

            RuleFor(book => book.Author).NotNull()
                                        .NotEmpty()
                                        .WithMessage("Author must be set");
        }
    }
}