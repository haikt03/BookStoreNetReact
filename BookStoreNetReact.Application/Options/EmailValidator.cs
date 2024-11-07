using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class EmailValidator : AbstractValidator<EmailOptions>
    {
        public EmailValidator() 
        {
            RuleFor(e => e.SmtpHost).NotEmpty();
            RuleFor(e => e.SmtpPort).NotEmpty();
            RuleFor(e => e.SmtpUser).NotEmpty();
            RuleFor(e => e.SmtpPass).NotEmpty();
            RuleFor(e => e.SmtpFrom).NotEmpty();
        }
    }
}
