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
                .InclusiveBetween(1832, DateTime.Now.Year);
            RuleFor(cb => cb.Language)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Weight)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1, int.MaxValue).WithMessage(ValidationErrorMessages.Range);
            RuleFor(cb => cb.NumberOfPages)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1, int.MaxValue).WithMessage(ValidationErrorMessages.Range);
            RuleFor(cb => cb.Form)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Description)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(50, 500).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cb => cb.Price)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1, int.MaxValue).WithMessage(ValidationErrorMessages.Range);
            RuleFor(cb => cb.Discount)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1, 100).WithMessage(ValidationErrorMessages.Range);
            RuleFor(cb => cb.QuantityInStock)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1, int.MaxValue).WithMessage(ValidationErrorMessages.Range);
            RuleFor(cb => cb.CategoryId)
                .InclusiveBetween(1, int.MaxValue).WithMessage(ValidationErrorMessages.Range);
            RuleFor(cb => cb.AuthorId)
                .InclusiveBetween(1, int.MaxValue).WithMessage(ValidationErrorMessages.Range);
        }
    }

    //public required string Name { get; set; }
    //public string? Translator { get; set; }
    //public required string Publisher { get; set; }
    //public required int PublishedYear { get; set; }
    //public required string Language { get; set; }
    //public required int Weight { get; set; }
    //public required int NumberOfPages { get; set; }
    //public required string Form { get; set; }
    //public required string Description { get; set; }
    //public required int Price { get; set; }
    //public required int Discount { get; set; }
    //public required int QuantityInStock { get; set; }
    //public int? CategoryId { get; set; }
    //public int? AuthorId { get; set; }
}
