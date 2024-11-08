using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(cc => cc.Name)
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Tên thể loại"));
            RuleFor(cc => cc.PId)
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Id thể loại cha"));
        }
    }
}
