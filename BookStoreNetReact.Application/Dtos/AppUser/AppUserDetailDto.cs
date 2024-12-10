using BookStoreNetReact.Application.Dtos.UserAddress;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class AppUserDetailDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public DateOnly DateOfBirth { get; set; }
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; } = "";
        public bool PhoneNumberConfirmed { get; set; }
        public string PublicId { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public UserAddressDto Address { get; set; } = new UserAddressDto();
    }
}
