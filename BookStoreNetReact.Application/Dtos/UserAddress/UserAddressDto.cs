namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class UserAddressDto
    {
        public required int Id { get; set; }
        public required string City { get; set; }
        public required string District { get; set; }
        public required string Ward { get; set; }
        public required string SpecificAddress { get; set; }
    }
}
