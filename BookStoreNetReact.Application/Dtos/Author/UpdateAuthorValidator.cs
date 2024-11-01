using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorValidator() 
        {
            RuleFor(ua => ua.Id)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(ua => ua.FullName)
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length);
            RuleFor(ua => ua.Biography)
                .Length(50, 500).WithMessage(ValidationErrorMessages.Length);
            RuleFor(ua => ua.Country)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
        }
    }
}
