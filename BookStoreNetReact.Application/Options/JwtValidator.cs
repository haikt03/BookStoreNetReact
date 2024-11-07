using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class JwtValidator : AbstractValidator<JwtOptions>
    {
        public JwtValidator()
        {
            RuleFor(x => x.TokenKey).NotEmpty();
            RuleFor(x => x.MinutesExpired).GreaterThanOrEqualTo(1);
            RuleFor(x => x.DaysExpired).GreaterThanOrEqualTo(1);
        }
    }
}
