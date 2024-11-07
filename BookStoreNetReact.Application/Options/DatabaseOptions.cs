namespace BookStoreNetReact.Application.Options
{
    public class DatabaseOptions
    {
        public const string DatabaseSettings = "DatabaseSettings";
        public required string ConnectionString { get; set; }
        public required int CommandTimeout { get; set; }
    }
}
