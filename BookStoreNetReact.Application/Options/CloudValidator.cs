using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class CloudValidator : AbstractValidator<CloudOptions>
    {
        public CloudValidator()
        {
            RuleFor(c => c.CloudName).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(c => c.ApiKey).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(c => c.ApiSecret).NotEmpty().WithMessage(ValidationErrorMessages.Required);
        }
    }
}
