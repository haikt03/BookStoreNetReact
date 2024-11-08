using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {
            RuleFor(cb => cb.Name)
                .Length(6, 250).WithMessage(ValidationErrorMessages.Length("Tên sách"));
            RuleFor(cb => cb.Translator)
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length("Người dịch"));
            RuleFor(cb => cb.Publisher)
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length("Nhà xuất bản"));
            RuleFor(cb => cb.PublishedYear)
                .InclusiveBetween(1800, DateTime.Now.Year).WithMessage(ValidationErrorMessages.InclusiveBetween("Năm xuất bản"));
            RuleFor(cb => cb.Language)
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Ngôn ngữ"));
            RuleFor(cb => cb.Weight)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Trọng lượng"));
            RuleFor(cb => cb.NumberOfPages)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Số trang"));
            RuleFor(cb => cb.Form)
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Hình thức"));
            RuleFor(cb => cb.Description)
                .Length(50, 500).WithMessage(ValidationErrorMessages.Length("Mô tả"));
            RuleFor(cb => cb.Price)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Giá"));
            RuleFor(cb => cb.Discount)
                .InclusiveBetween(0, 100).WithMessage(ValidationErrorMessages.InclusiveBetween("Giảm giá"));
            RuleFor(cb => cb.QuantityInStock)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Số lượng tồn kho"));
            RuleFor(cb => cb.CategoryId)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Id thể loại"));
            RuleFor(cb => cb.AuthorId)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Id tác giả"));
        }
    }
}
