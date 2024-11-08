using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorValidator() 
        {
            RuleFor(ua => ua.FullName)
                .Length(1, 100).WithMessage(ValidationErrorMessages.Length("Họ và tên"));
            RuleFor(ua => ua.Biography)
                .Length(50, 500).WithMessage(ValidationErrorMessages.Length("Tiểu sử"));
            RuleFor(ua => ua.Country)
                .Length(1, 50).WithMessage(ValidationErrorMessages.Length("Nơi sinh"));
        }
    }
}
