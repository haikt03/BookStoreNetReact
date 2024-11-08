using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(cc => cc.Name)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Tên thể loại"))
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Tên thể loại"));
            RuleFor(cc => cc.PId)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Id thể loại cha"))
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Id thể loại cha"));
        }
    }
}
