using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class DatabaseValidator : AbstractValidator<DatabaseOptions>
    {
        public DatabaseValidator()
        {
            RuleFor(c => c.ConnectionString).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(c => c.CommandTimeout)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
        }
    }
}
