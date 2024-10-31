using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(r => r.DateOfBirth)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Now)).WithMessage(ValidationErrorMessages.Date);
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .EmailAddress().WithMessage(ValidationErrorMessages.Email);
            RuleFor(r => r.PhoneNumber)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Matches(@"^0\d{9}$|^\+84\d{10}$").WithMessage(ValidationErrorMessages.PhoneNumber);
        }
    }
}
