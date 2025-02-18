namespace BookStoreNetReact.Application.Options
{
    public class CachingOptions
    {
        public const string RedisSettings = "RedisSettings";
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required string Password { get; set; }
        public required int Database  { get; set; }
    }
}
