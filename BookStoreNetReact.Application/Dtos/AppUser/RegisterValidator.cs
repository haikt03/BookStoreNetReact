using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Tên người dùng"))
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Tên người dùng"));
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Email"))
                .EmailAddress().WithMessage(ValidationErrorMessages.EmailAddress);
            RuleFor(r => r.PhoneNumber)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Số điện thoại"))
                .Matches(@"^0\d{9}$|^\+84\d{10}$").WithMessage(ValidationErrorMessages.PhoneNumber);
            RuleFor(r => r.FullName)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Họ và tên"))
                .Length(6, 100).WithMessage(ValidationErrorMessages.Length("Họ và tên"));
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Mật khẩu"))
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Mật khẩu"));
            RuleFor(r => r.DateOfBirth)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Ngày sinh"))
                .Must(BeAValidDate).WithMessage(ValidationErrorMessages.Invalid("Ngày sinh"))
                .Must(BeAtLeast18YearsOld).WithMessage("Bạn phải đủ 18 tuổi.");
        }

        private bool BeAValidDate(string dateOfBirth)
        {
            return DateOnly.TryParse(dateOfBirth, out var parsedDate) &&
                   parsedDate <= DateOnly.FromDateTime(DateTime.Today);
        }

        private bool BeAtLeast18YearsOld(string dateOfBirth)
        {
            if (!DateOnly.TryParse(dateOfBirth, out var parsedDate))
                return false;
            var minAgeDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
            return parsedDate <= minAgeDate;
        }
    }
}
