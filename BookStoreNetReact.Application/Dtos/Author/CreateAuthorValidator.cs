using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorValidator() 
        {
            RuleFor(ca => ca.FullName)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Họ và tên"))
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length("Họ và tên"));
            RuleFor(ca => ca.Biography)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Tiểu sử"))
                .Length(50, 500).WithMessage(ValidationErrorMessages.Length("Tiểu sử"));
            RuleFor(ca => ca.Country)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty("Nơi sinh"))
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Nơi sinh"));
        }
    }
}
