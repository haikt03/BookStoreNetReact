using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Tên người dùng"))
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Tên người dùng"));
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Mật khẩu"))
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Mật khẩu"));
        }
    }
}
