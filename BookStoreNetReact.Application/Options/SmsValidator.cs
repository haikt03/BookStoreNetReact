using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class SmsValidator : AbstractValidator<SmsOptions>
    {
        public SmsValidator() 
        {
            RuleFor(c => c.PhoneNumber).NotEmpty();
            RuleFor(c => c.AuthToken).NotEmpty();
            RuleFor(c => c.AccountSid).NotEmpty();
        }
    }
}
