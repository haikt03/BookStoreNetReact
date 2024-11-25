namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class CreateUserAddresssDto
    {
        public required string City { get; set; }
        public required string District { get; set; }
        public required string Ward { get; set; }
        public required string SpecificAddress { get; set; }
    }
}
