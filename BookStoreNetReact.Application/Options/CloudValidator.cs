using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class CloudValidator : AbstractValidator<CloudOptions>
    {
        public CloudValidator()
        {
            RuleFor(c => c.CloudName).NotEmpty();
            RuleFor(c => c.ApiKey).NotEmpty();
            RuleFor(c => c.ApiSecret).NotEmpty();
        }
    }
}
