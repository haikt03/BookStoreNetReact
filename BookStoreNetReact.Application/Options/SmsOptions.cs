namespace BookStoreNetReact.Application.Options
{
    public class SmsOptions
    {
        public const string TwilioSettings = "TwilioSettings";
        public required string PhoneNumber { get; set; }
        public required string AuthToken { get; set; }
        public required string AccountSid { get; set; }
    }
}
