namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class CreateUserAddressDto
    {
        public required string City { get; set; }
        public required string District { get; set; }
        public required string Ward { get; set; }
        public required string Street { get; set; }
        public required string Alley { get; set; }
        public required string HouseNumber { get; set; }
    }
}