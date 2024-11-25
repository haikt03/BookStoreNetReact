using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class CreateUserAddressValidator : AbstractValidator<CreateUserAddresssDto>
    {
        public CreateUserAddressValidator() 
        {
            RuleFor(uu => uu.City)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Tỉnh/Thành phố"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Tỉnh/Thành phố"));
            RuleFor(uu => uu.District)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Quận/Huyện"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Quận/Huyện"));
            RuleFor(uu => uu.Ward)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Phường/Xã"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Phường/Xã"));
            RuleFor(uu => uu.SpecificAddress)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Địa chỉ cụ thể"))
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length("Địa chỉ cụ thể"));
        }
    }
}
