using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {
            RuleFor(cb => cb.Name)
                .Length(6, 250).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Translator)
                .Length(1, 100).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Publisher)
                .Length(1, 100).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.PublishedYear)
                .InclusiveBetween(1800, DateTime.Now.Year).WithMessage(ValidationErrorMessages.ValidRange);
            RuleFor(cb => cb.Language)
                .Length(6, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Weight)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo);
            RuleFor(cb => cb.NumberOfPages)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo);
            RuleFor(cb => cb.Form)
                .Length(6, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Description)
                .Length(50, 500).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Price)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo);
            RuleFor(cb => cb.Discount)
                .InclusiveBetween(0, 100).WithMessage(ValidationErrorMessages.ValidRange);
            RuleFor(cb => cb.QuantityInStock)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo);
            RuleFor(cb => cb.CategoryId)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo);
            RuleFor(cb => cb.AuthorId)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo);
        }
    }
}
