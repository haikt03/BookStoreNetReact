using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorValidator() 
        {
            Include(new CreateAuthorValidator());
            RuleFor(ua => ua.Id)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1, int.MinValue).WithMessage(ValidationErrorMessages.Range);
        }
    }
}
