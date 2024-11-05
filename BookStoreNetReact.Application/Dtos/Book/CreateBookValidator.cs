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
                .Length(1, 250).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Translator)
                .Length(1, 100).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Publisher)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 100).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.PublishedYear)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1832, DateTime.Now.Year).WithMessage(ValidationErrorMessages.ValidRange);
            RuleFor(cb => cb.Language)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Weight)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
            RuleFor(cb => cb.NumberOfPages)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
            RuleFor(cb => cb.Form)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Description)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(50, 500).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cb => cb.Price)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
            RuleFor(cb => cb.Discount)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(0, 100).WithMessage(ValidationErrorMessages.ValidRange);
            RuleFor(cb => cb.QuantityInStock)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
            RuleFor(cb => cb.CategoryId)
                .GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
            RuleFor(cb => cb.AuthorId)
                .GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
        }
    }
}
