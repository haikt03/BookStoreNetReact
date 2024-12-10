namespace BookStoreNetReact.Application.Dtos.UserAddress
{
    public class UserAddressDto
    {
        public int Id { get; set; }
        public string City { get; set; } = "";
        public string District { get; set; } = "";
        public string Ward { get; set; } = "";
        public string SpecificAddress { get; set; } = "";
    }
}
