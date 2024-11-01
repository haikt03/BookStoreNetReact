using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class JwtValidator : AbstractValidator<JwtOptions>
    {
        public JwtValidator()
        {
            RuleFor(x => x.TokenKey).NotEmpty();
            RuleFor(x => x.MinutesExpired).GreaterThan(0);
            RuleFor(x => x.DaysExpired).GreaterThan(0);
        }
    }
}
