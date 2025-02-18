using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class CachingValidator : AbstractValidator<CachingOptions>
    {
        public CachingValidator()
        {
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}
