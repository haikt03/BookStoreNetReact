using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class UpdateUserAddressValidator : AbstractValidator<UpdateUserAddressDto>
    {
        public UpdateUserAddressValidator() 
        {
            RuleFor(uu => uu.City)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(uu => uu.District)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(uu => uu.Ward)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(uu => uu.Street)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(uu => uu.Alley)
                .Length(1, 50).WithMessage(ValidationErrorMessages.ValidLength);
            RuleFor(uu => uu.HouseNumber)
                .Length(1, 5).WithMessage(ValidationErrorMessages.ValidLength);
        }
    }
}
