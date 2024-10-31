using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Options
{
    public class JwtOptions
    {
        public const string JwtBearerOptions = "JwtBearerOptions";
        [Required(AllowEmptyStrings = false)]
        public required string TokenKey { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public required int MinutesExpired { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public required int DaysExpired { get; set; }
    }
}
