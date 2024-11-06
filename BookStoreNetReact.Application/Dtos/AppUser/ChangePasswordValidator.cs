using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator() 
        {
            RuleFor(cp => cp.CurrentPassword)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(6, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(cp => cp.NewPassword)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(6, 50).WithMessage(ValidationErrorMessages.ValidLength);
        }
    }
}
