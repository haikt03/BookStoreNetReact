using FluentValidation;

namespace BookStoreNetReact.Application.Options
{
    public class PaymentValidator : AbstractValidator<PaymentOptions>
    {
        public PaymentValidator()
        {
            RuleFor(c => c.PublishableKey).NotEmpty();
            RuleFor(c => c.SecretKey).NotEmpty();
            RuleFor(c => c.WhSecret).NotEmpty();
        }
    }
}
