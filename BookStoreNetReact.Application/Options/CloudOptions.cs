namespace BookStoreNetReact.Application.Options
{
    public class CloudOptions
    {
        public const string CloudinarySettings = "CloudinarySettings";
        public required string CloudName { get; set; }
        public required string ApiKey { get; set; }
        public required string ApiSecret { get; set; }
    }
}
