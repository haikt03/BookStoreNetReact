using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {
            RuleFor(ub => ub.Id)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.Name)
                .Length(1, 250).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Translator)
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Publisher)
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.PublishedYear)
                .InclusiveBetween(1832, DateTime.Now.Year);
            RuleFor(cb => cb.Language)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Weight)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.NumberOfPages)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.Form)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Description)
                .Length(50, 500).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Price)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.Discount)
                .InclusiveBetween(1, 100).WithMessage(ValidationErrorMessages.Range);
            RuleFor(cb => cb.QuantityInStock)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.CategoryId)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cb => cb.AuthorId)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
        }
    }
}
