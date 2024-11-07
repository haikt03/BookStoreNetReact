using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class EmailValidator : AbstractValidator<EmailOptions>
    {
        public EmailValidator() 
        {
            RuleFor(e => e.SmtpHost).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(e => e.SmtpPort).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(e => e.SmtpUser).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(e => e.SmtpPass).NotEmpty().WithMessage(ValidationErrorMessages.Required);
            RuleFor(e => e.SmtpFrom).NotEmpty().WithMessage(ValidationErrorMessages.Required);
        }
    }
}
