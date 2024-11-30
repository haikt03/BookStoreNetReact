using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class UpdateAppUserValidator : AbstractValidator<UpdateAppUserDto>
    {
        public UpdateAppUserValidator()
        {
            RuleFor(ua => ua.FullName)
                .Length(6, 100).WithMessage(ValidationErrorMessages.Length("Họ và tên"));
            RuleFor(ua => ua.DateOfBirth)
                .Must(dob => dob >= new DateOnly(1900, 1, 1)).WithMessage(ValidationErrorMessages.Invalid("Ngày sinh"))
                .Must(dob => dob <= DateOnly.FromDateTime(DateTime.Today.AddYears(-18))).WithMessage("Bạn phải đủ 18 tuổi");
            RuleFor(ua => ua.Email)
                .EmailAddress().WithMessage(ValidationErrorMessages.EmailAddress);
            RuleFor(ua => ua.PhoneNumber)
                .Matches(@"^0\d{9}$|^\+84\d{10}$").WithMessage(ValidationErrorMessages.PhoneNumber);
        }
    }
}
