using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Options
{
    public class DatabaseOptions
    {
        public const string SqlServerOptions = "SqlServerOptions";
        public required string ConnectionString { get; set; }
        public required int CommandTimeout { get; set; }
    }
}
