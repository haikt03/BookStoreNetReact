using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorValidator() 
        {
            RuleFor(ua => ua.FullName)
                .Must((dto, value) => value == null || value.Length >= 1 || value.Length <= 100)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(1, 100));
            RuleFor(ua => ua.Biography)
                .Must((dto, value) => value == null || value.Length >= 50 || value.Length <= 500)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(50, 500));
            RuleFor(ua => ua.Country)
                .Must((dto, value) => value == null || value.Length >= 1 || value.Length <= 50)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(1, 50));
        }
    }
}
