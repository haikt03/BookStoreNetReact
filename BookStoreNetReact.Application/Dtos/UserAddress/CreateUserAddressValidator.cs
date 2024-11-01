using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class CreateUserAddressValidator : AbstractValidator<CreateUserAddressDto>
    {
        public CreateUserAddressValidator()
        {
            RuleFor(cu => cu.City)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cu => cu.District)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cu => cu.Ward)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cu => cu.Street)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cu => cu.Alley)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cu => cu.HouseNumber)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .Length(1, 5).WithMessage(ValidationErrorMessages.Length);
            RuleFor(cu => cu.UserId)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .GreaterThan(1).WithMessage(ValidationErrorMessages.Greater);
        }
    }
}
