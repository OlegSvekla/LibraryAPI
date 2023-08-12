using FluentValidation;
using LibraryAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Core.Validation
{
    public class AuthorDtoValidator : AbstractValidator<AuthorDto>
    {
        //TODO REWRITE
        public AuthorDtoValidator()
        {
            RuleFor(author => author.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(author => author.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
        }
    }
}
