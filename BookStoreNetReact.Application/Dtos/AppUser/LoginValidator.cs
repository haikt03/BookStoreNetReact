using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(6, 50).WithMessage(ValidationErrorMessages.ValidLength);
        }
    }
}
