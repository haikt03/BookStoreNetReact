﻿using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class UpdateUserAddressValidator : AbstractValidator<UpdateUserAddressDto>
    {
        public UpdateUserAddressValidator() 
        {
            RuleFor(uu => uu.City)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Tỉnh/Thành phố"));
            RuleFor(uu => uu.District)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Quận/Huyện"));
            RuleFor(uu => uu.Ward)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Phường/Xã"));
            RuleFor(uu => uu.Street)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Đường/Phố"));
            RuleFor(uu => uu.Alley)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Ngõ/Ngách/Hẻm"));
            RuleFor(uu => uu.HouseNumber)
                .Length(1, 5).WithMessage(ValidationErrorMessages.Length("Số nhà"));
        }
    }
}
