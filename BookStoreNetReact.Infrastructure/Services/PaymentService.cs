using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using BookStoreNetReact.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IOptions<PaymentOptions> _paymentOptions;
        private readonly ILogger<CloudUploadService> _logger;
        public PaymentService(IOptions<PaymentOptions> paymentOptions, ILogger<CloudUploadService> logger)
        {
            _paymentOptions = paymentOptions;
            _logger = logger;
        }
        public async Task<PaymentIntent?> CreateOrUpdatePaymentIntent(Basket basket)
        {
            try
            {
                StripeConfiguration.ApiKey = _paymentOptions.Value.SecretKey;
                var service = new PaymentIntentService();
                var intent = new PaymentIntent();

                var subtotal = basket.Items.Sum(i => i.Quantity * i?.Book?.Price);
                var deliveryFee = subtotal > 200000 ? 0 : 30000;

                if (string.IsNullOrEmpty(basket.PaymentIntentId))
                {
                    var options = new PaymentIntentCreateOptions
                    {
                        Amount = subtotal + deliveryFee,
                        Currency = "VND",
                        PaymentMethodTypes = new List<string> { "card" }
                    };
                    intent = await service.CreateAsync(options);
                    basket.PaymentIntentId = intent.Id;
                    basket.ClientSecret = intent.ClientSecret;
                }
                else
                {
                    var currentIntent = await service.GetAsync(basket.PaymentIntentId);

                    if (currentIntent.Status == "succeeded")
                    {
                        var options = new PaymentIntentCreateOptions
                        {
                            Amount = subtotal + deliveryFee,
                            Currency = "VND",
                            PaymentMethodTypes = new List<string> { "card" }
                        };
                        intent = await service.CreateAsync(options);
                        basket.PaymentIntentId = intent.Id;
                        basket.ClientSecret = intent.ClientSecret;
                    }
                    else
                    {
                        var options = new PaymentIntentUpdateOptions
                        {
                            Amount = subtotal + deliveryFee
                        };
                        intent = await service.UpdateAsync(basket.PaymentIntentId, options);
                    }
                }

                return intent;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while creating or updating payment intent");
                return null;
            }
        }
    }
}
