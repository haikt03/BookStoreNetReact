using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class UpdateAppUserValidator : AbstractValidator<UpdateAppUserDto>
    {
        public UpdateAppUserValidator()
        {
            RuleFor(ua => ua.Id)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
            RuleFor(ua => ua.FirstName)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(ua => ua.LastName)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(ua => ua.DateOfBirth)
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Now)).WithMessage(ValidationErrorMessages.Date);
            RuleFor(ua => ua.UserName)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(ua => ua.Password)
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(ua => ua.Email)
                .EmailAddress().WithMessage(ValidationErrorMessages.Email);
            RuleFor(ua => ua.PhoneNumber)
                .Matches(@"^0\d{9}$|^\+84\d{10}$").WithMessage(ValidationErrorMessages.PhoneNumber);
        }
    }
}
