using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator()
        {
            RuleFor(cb => cb.Name)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Tên sách"))
                .Length(6, 250).WithMessage(ValidationErrorMessages.Length("Tên sách"));
            RuleFor(cb => cb.Translator)
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length("Người dịch"));
            RuleFor(cb => cb.Publisher)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Nhà xuất bản"))
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length("Nhà xuất bản"));
            RuleFor(cb => cb.PublishedYear)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Năm xuất bản"))
                .InclusiveBetween(1800, DateTime.Now.Year).WithMessage(ValidationErrorMessages.InclusiveBetween("Năm xuất bản"));
            RuleFor(cb => cb.Language)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Ngôn ngữ"))
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Ngôn ngữ"));
            RuleFor(cb => cb.Weight)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Trọng lượng"))
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Trọng lượng"));
            RuleFor(cb => cb.NumberOfPages)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Số trang"))
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Số trang"));
            RuleFor(cb => cb.Form)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Hình thức"))
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Hình thức"));
            RuleFor(cb => cb.Description)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Mô tả"))
                .Length(50, 500).WithMessage(ValidationErrorMessages.Length("Mô tả"));
            RuleFor(cb => cb.Price)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Giá"))
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Giá"));
            RuleFor(cb => cb.Discount)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Giảm giá"))
                .InclusiveBetween(0, 100).WithMessage(ValidationErrorMessages.InclusiveBetween("Giảm giá"));
            RuleFor(cb => cb.QuantityInStock)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Số lượng tồn kho"))
                .GreaterThanOrEqualTo(0).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Số lượng tồn kho"));
            RuleFor(cb => cb.CategoryId)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Id thể loại"));
            RuleFor(cb => cb.AuthorId)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Id tác giả"));
        }
    }
}
