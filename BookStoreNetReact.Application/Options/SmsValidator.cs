using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class SmsValidator : AbstractValidator<SmsOptions>
    {
        public SmsValidator() 
        {
            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(c => c.AuthToken).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(c => c.AccountSid).NotEmpty().WithMessage(ValidationErrorMessages.Required);
        }
    }
}
