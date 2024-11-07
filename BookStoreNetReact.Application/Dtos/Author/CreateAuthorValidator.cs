using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorValidator() 
        {
            RuleFor(ca => ca.FullName)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 100).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(ca => ca.Biography)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(50, 500).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(ca => ca.Country)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
        }
    }
}
