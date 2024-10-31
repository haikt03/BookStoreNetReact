using BookStoreNetReact.Application.Dtos.UserAddress;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class AppUserDto
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public UserAddressDto? Address { get; set; }
    }
}
