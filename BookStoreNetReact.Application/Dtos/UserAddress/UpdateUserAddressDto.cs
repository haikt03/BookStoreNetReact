namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class UpdateUserAddressDto
    {
        public required int Id { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Street { get; set; }
        public string? Alley { get; set; }
        public string? HouseNumber { get; set; }
    }
}
