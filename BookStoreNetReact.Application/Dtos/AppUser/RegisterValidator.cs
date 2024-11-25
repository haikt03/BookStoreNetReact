using BookStoreNetReact.Application.Dtos.UserAddress;
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
                .Must(dob => dob >= new DateOnly(1900, 1, 1)).WithMessage(ValidationErrorMessages.Invalid("Ngày sinh"))
                .Must(dob => dob <= DateOnly.FromDateTime(DateTime.Today.AddYears(-18))).WithMessage("Bạn phải đủ 18 tuổi");
            RuleFor(r => r.Address)
                .SetValidator(new CreateUserAddressValidator());
        }
    }
}
