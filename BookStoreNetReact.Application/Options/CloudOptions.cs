using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Options
{
    public class CloudOptions
    {
        public const string CloudinaryOptions = "CloudinaryOptions";
        [Required(AllowEmptyStrings = false)]
        public required string CloudName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public required string ApiKey { get; set; }
        [Required(AllowEmptyStrings = false)]
        public required string ApiSecret { get; set; }
    }
}
