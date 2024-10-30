namespace BookStoreNetReact.Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        public required string City { get; set; }
        public required string District { get; set; }
        public required string Ward { get; set; }
        public required string Street { get; set; }
        public required string Alley { get; set; }
        public required string HouseNumber { get; set; }
        public int? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
