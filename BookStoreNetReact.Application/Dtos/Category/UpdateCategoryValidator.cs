using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(cc => cc.Id)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(cc => cc.Name)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cc => cc.PId)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
        }
    }
}
