using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class LoginRequestDto : BaseDto
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(6, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string UserName { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(6, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string Password { get; set; }
    }
}
