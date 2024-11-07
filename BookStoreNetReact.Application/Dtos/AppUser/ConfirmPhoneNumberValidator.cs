using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class ConfirmPhoneNumberValidator : AbstractValidator<ConfirmPhoneNumberDto>
    {
        public ConfirmPhoneNumberValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(ValidationErrorMessages.Required);
        }
    }
}
