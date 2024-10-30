using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Options
{
    public class DatabaseOptions
    {
        public const string SqlServerOptions = "SqlServerOptions";
        [Required(AllowEmptyStrings = false)]
        public required string ConnectionString { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public required int CommandTimeout { get; set; }
    }
}
