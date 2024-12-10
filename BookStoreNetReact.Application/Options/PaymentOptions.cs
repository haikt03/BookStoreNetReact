namespace BookStoreNetReact.Application.Options
{
    public class PaymentOptions
    {
        public const string StripeSettings = "StripeSettings";
        public required string PublishableKey { get; set; }
        public required string SecretKey { get; set; }
        public required string WhSecret { get; set; }
    }
}
