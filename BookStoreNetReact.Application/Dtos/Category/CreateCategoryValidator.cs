using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(cc => cc.Name)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cc => cc.PId)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(0).WithMessage(ValidationErrorMessages.GreaterThan);
        }
    }
}
