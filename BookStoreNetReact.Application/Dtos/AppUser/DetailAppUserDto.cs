using BookStoreNetReact.Application.Dtos.UserAddress;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class DetailAppUserDto
    {
        public required int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required bool EmailConfirmed { get; set; }
        public required string PhoneNumber { get; set; }
        public required bool PhoneNumberConfirmed { get; set; }
        public required string FullName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public UserAddressDto? Address { get; set; }
    }
}
