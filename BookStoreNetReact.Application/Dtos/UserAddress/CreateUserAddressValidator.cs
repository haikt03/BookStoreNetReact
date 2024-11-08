using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class CreateUserAddressValidator : AbstractValidator<CreateUserAddressDto>
    {
        public CreateUserAddressValidator()
        {
            RuleFor(cu => cu.City)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Tỉnh/Thành phố"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Tỉnh/Thành phố"));
            RuleFor(cu => cu.District)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Quận/Huyện"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Quận/Huyện"));
            RuleFor(cu => cu.Ward)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Phường/Xã"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Phường/Xã"));
            RuleFor(cu => cu.Street)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Đường/Phố"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Đường/Phố"));
            RuleFor(cu => cu.Alley)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Ngõ/Ngách/Hẻm"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Ngõ/Ngách/Hẻm"));
            RuleFor(cu => cu.HouseNumber)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Số nhà"))
                .Length(1, 5).WithMessage(ValidationErrorMessages.Length("Số nhà"));
        }
    }
}
