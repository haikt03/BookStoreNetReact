using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Basket
{
    public class UpdateQuantityValidator: AbstractValidator<UpdateQuantityDto>
    {
        public UpdateQuantityValidator() 
        {
            RuleFor(ua => ua.BookId)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Id sách"))
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Id sách"));
            RuleFor(ua => ua.Quantity)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Số lượng"))
                .GreaterThanOrEqualTo(1).WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo("Số lượng"));
        }
    }
}
