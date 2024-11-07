using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class JwtValidator : AbstractValidator<JwtOptions>
    {
        public JwtValidator()
        {
            RuleFor(x => x.TokenKey).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(x => x.MinutesExpired).GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
            RuleFor(x => x.DaysExpired).GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
        }
    }
}
