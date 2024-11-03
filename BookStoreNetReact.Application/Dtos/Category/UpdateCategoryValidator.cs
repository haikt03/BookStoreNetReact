using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(cc => cc.Name)
                .Must((dto, value) => value == null || value.Length >= 1 || value.Length <= 50)
                .WithMessage(ValidationErrorMessages.BeNullOrValidLength(1, 50));
            RuleFor(cc => cc.PId)
                .Must((dto, value) => value == null || value >= 1)
                .WithMessage(ValidationErrorMessages.BeNullOrGreaterThan(1));
        }
    }
}
