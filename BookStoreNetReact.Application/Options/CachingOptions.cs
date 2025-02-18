namespace BookStoreNetReact.Application.Options
{
    public class CachingOptions
    {
        public const string RedisSettings = "RedisSettings";
        public required string Password { get; set; }
    }
}
