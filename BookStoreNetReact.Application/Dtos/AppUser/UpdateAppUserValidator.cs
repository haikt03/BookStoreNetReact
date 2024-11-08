﻿using BookStoreNetReact.Application.Helpers;
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
                .Must(dob => dob == null || (dob >= new DateOnly(1900, 1, 1) && dob <= DateOnly.FromDateTime(DateTime.Now)))
                .WithMessage(ValidationErrorMessages.DateOfBirth);
            RuleFor(ua => ua.UserName)
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Tên người dùng"));
            RuleFor(ua => ua.Password)
                .Length(6, 50).WithMessage(ValidationErrorMessages.Length("Mật khẩu"));
            RuleFor(ua => ua.Email)
                .EmailAddress().WithMessage(ValidationErrorMessages.EmailAddress);
            RuleFor(ua => ua.PhoneNumber)
                .Matches(@"^0\d{9}$|^\+84\d{10}$").WithMessage(ValidationErrorMessages.PhoneNumber);
        }
    }
}
