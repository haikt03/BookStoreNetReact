using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class UpdateAppUserValidator : AbstractValidator<UpdateAppUserDto>
    {
        public UpdateAppUserValidator()
        {
            RuleFor(ua => ua.FirstName)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(ua => ua.LastName)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(ua => ua.DateOfBirth)
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Now)).WithMessage(ValidationErrorMessages.ValidDate);
            RuleFor(ua => ua.UserName)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(ua => ua.Password)
                .Length(6, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(ua => ua.Email)
                .EmailAddress().WithMessage(ValidationErrorMessages.ValidEmail);
            RuleFor(ua => ua.PhoneNumber)
                .Matches(@"^0\d{9}$|^\+84\d{10}$").WithMessage(ValidationErrorMessages.ValidPhoneNumber);
        }
    }
}
