using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Options
{
    public class JwtOptions
    {
        public const string JwtBearerOptions = "JwtBearerOptions";
        [Required(AllowEmptyStrings = false)]
        public required string TokenKey { get; set; }
    }
}
