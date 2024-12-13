﻿using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Order
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(sa => sa.ShippingAddress.City)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Tỉnh/Thành phố"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Tỉnh/Thành phố"));
            RuleFor(sa => sa.ShippingAddress.District)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Quận/Huyện"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Quận/Huyện"));
            RuleFor(sa => sa.ShippingAddress.Ward)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Phường/Xã"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Phường/Xã"));
            RuleFor(sa => sa.ShippingAddress.SpecificAddress)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Địa chỉ cụ thể"))
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length("Địa chỉ cụ thể"));
        }
    }
}