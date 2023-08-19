using FluentValidation;
using LibraryAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Domain.Validation
{
    public class AuthorDtoValidator : AbstractValidator<AuthorDto>
    {
        public AuthorDtoValidator()
        {
            RuleFor(author => author.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(25).WithMessage("First name must not exceed 25 characters.");

            RuleFor(author => author.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(25).WithMessage("Last name must not exceed 25 characters.");
        }
    }
}