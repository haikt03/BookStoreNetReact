using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class CachingValidator : AbstractValidator<CachingOptions>
    {
        public CachingValidator()
        {
            RuleFor(c => c.Host).NotEmpty();
            RuleFor(c => c.Port).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.Database).NotEmpty();
        }
    }
}
