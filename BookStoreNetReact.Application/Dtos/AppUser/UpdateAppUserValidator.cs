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
                .Must(BeAValidDate).WithMessage(ValidationErrorMessages.Invalid("Ngày sinh"))
                .Must(BeAtLeast18YearsOld).WithMessage("Bạn phải đủ 18 tuổi.");
            RuleFor(ua => ua.UserName)
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Tên người dùng"));
            RuleFor(ua => ua.Password)
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Mật khẩu"));
            RuleFor(ua => ua.Email)
                .EmailAddress().WithMessage(ValidationErrorMessages.EmailAddress);
            RuleFor(ua => ua.PhoneNumber)
                .Matches(@"^0\d{9}$|^\+84\d{10}$").WithMessage(ValidationErrorMessages.PhoneNumber);
        }

        private bool BeAValidDate(string? dateOfBirth)
        {
            if (dateOfBirth == null)
                return false;
            return DateOnly.TryParse(dateOfBirth, out var parsedDate) &&
                   parsedDate <= DateOnly.FromDateTime(DateTime.Today);
        }

        private bool BeAtLeast18YearsOld(string? dateOfBirth)
        {
            if (dateOfBirth == null)
                return false;
            if (!DateOnly.TryParse(dateOfBirth, out var parsedDate))
                return false;
            var minAgeDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
            return parsedDate <= minAgeDate;
        }
    }
}
