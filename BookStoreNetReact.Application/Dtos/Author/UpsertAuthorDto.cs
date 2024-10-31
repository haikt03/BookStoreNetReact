using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class UpsertAuthorDto : BaseDto
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string FullName { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(10, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(500, ErrorMessage = MaxLengthErrorMessage)]
        public required string Biography { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string Country { get; set; }
    }
}
