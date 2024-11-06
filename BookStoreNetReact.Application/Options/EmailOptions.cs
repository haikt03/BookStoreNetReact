namespace BookStoreNetReact.Application.Options
{
    public class EmailOptions
    {
        public const string EmailSettings = "EmailSettings";
        public required string SmtpHost { get; set; }
        public required string SmtpPort { get; set; }
        public required string SmtpUser { get; set; }
        public required string SmtpPass { get; set; }
        public required string SmtpFrom { get; set; }
    }
}
