using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator() 
        {
            RuleFor(cp => cp.CurrentPassword)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Mật khẩu hiện tại"))
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Mật khẩu hiện tại"));
            RuleFor(cp => cp.NewPassword)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Mật khẩu mới"))
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Mật khẩu mới"));
        }
    }
}
