using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {
            RuleFor(cb => cb.Name)
                .Must((dto, value) => value == null || value.Length >= 1 || value.Length <= 250)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(1, 250));
            RuleFor(cb => cb.Translator)
                .Must((dto, value) => value == null || value.Length >= 1 || value.Length <= 100)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(1, 100));
            RuleFor(cb => cb.Publisher)
                .Must((dto, value) => value == null || value.Length >= 1 || value.Length <= 100)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(1, 100));
            RuleFor(cb => cb.PublishedYear)
                .Must((dto, value) => value == null || value >= 1832 || value <= DateTime.Now.Year)
                .WithMessage(ValidationErrorMessages.BeNullOrValidRange(1832, DateTime.Now.Year));
            RuleFor(cb => cb.Language)
                .Must((dto, value) => value == null || value.Length >= 1 || value.Length <= 50)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(1, 50));
            RuleFor(cb => cb.Weight)
                .Must((dto, value) => value == null || value >= 1)
                .WithMessage(ValidationErrorMessages.BeNullOrGreaterThan(1));
            RuleFor(cb => cb.NumberOfPages)
                .Must((dto, value) => value == null || value >= 1)
                .WithMessage(ValidationErrorMessages.BeNullOrGreaterThan(1));
            RuleFor(cb => cb.Form)
                .Must((dto, value) => value == null || value.Length >= 1 || value.Length <= 50)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(1, 50));
            RuleFor(cb => cb.Description)
                .Must((dto, value) => value == null || value.Length >= 50 || value.Length <= 500)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(50, 500));
            RuleFor(cb => cb.Price)
                .Must((dto, value) => value == null || value >= 1)
                .WithMessage(ValidationErrorMessages.BeNullOrGreaterThan(1));
            RuleFor(cb => cb.Discount)
                .Must((dto, value) => value == null || value >= 0 || value <= 100)
                .WithMessage(ValidationErrorMessages.BeNullOrValidRange(0, 100));
            RuleFor(cb => cb.QuantityInStock)
                .Must((dto, value) => value == null || value >= 1)
                .WithMessage(ValidationErrorMessages.BeNullOrGreaterThan(1));
            RuleFor(cb => cb.CategoryId)
                .Must((dto, value) => value == null || value >= 1)
                .WithMessage(ValidationErrorMessages.BeNullOrGreaterThan(1));
            RuleFor(cb => cb.AuthorId)
                .Must((dto, value) => value == null || value >= 1)
                .WithMessage(ValidationErrorMessages.BeNullOrGreaterThan(1));
        }
    }
}
