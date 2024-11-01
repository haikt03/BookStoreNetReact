using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator()
        {
            RuleFor(cb => cb.Name)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 250).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Translator)
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Publisher)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.PublishedYear)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1832, DateTime.Now.Year).WithMessage(ValidationErrorMessages.Range);
            RuleFor(cb => cb.Language)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Weight)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.NumberOfPages)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.Form)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Description)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(50, 500).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Price)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.Discount)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1, 100).WithMessage(ValidationErrorMessages.Range);
            RuleFor(cb => cb.QuantityInStock)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.CategoryId)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.AuthorId)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
        }
    }
}
