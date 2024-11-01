using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Options
{
    public class JwtOptions
    {
        public const string JwtBearerOptions = "JwtBearerOptions";
        public required string TokenKey { get; set; }
        public required int MinutesExpired { get; set; }
        public required int DaysExpired { get; set; }
    }
}
