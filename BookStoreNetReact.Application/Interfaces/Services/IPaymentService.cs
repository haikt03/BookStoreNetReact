using BookStoreNetReact.Domain.Entities;
using Stripe;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<PaymentIntent?> GeneratePaymentIntent(Basket basket);
    }
}
