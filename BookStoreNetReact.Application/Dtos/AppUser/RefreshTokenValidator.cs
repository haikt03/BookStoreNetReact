using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenValidator() 
        {
            RuleFor(r => r.RefreshToken).NotEmpty().WithMessage(ValidationErrorMessages.Required);
        }
    }
}
