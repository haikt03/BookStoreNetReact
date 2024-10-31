using BookStoreNetReact.Application.Dtos.UserAddress;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class RegisterRequestDto : BaseDto
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(1, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string LastName { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        public required DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(6, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string UserName { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(6, ErrorMessage = MinLengthErrorMessage)]
        [MaxLength(50, ErrorMessage = MaxLengthErrorMessage)]
        public required string Password { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = EmailErrorMessage)]
        public required string Email { get; set; }
        [Phone(ErrorMessage = PhoneErrorMessage)]
        public required string PhoneNumber { get; set; }
    }
}
