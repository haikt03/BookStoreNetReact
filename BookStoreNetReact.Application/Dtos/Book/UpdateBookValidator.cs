using BookStoreNetReact.Application.Helpers;
using FluentValidation;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator() 
        {
            Include(new CreateBookValidator());
            RuleFor(ub => ub.Id)
                .NotEmpty().WithMessage(ValidationErrorMessages.Required)
                .InclusiveBetween(1, int.MinValue).WithMessage(ValidationErrorMessages.Range);
        }
    }
}
