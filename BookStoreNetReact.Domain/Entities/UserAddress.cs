using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        [StringLength(50, MinimumLength = 1)]
        public required string City { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public required string District { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public required string Ward { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public required string Street { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public required string Alley { get; set; }
        [StringLength(5, MinimumLength = 1)]
        public required string HouseNumber { get; set; }
        [Range(1, int.MaxValue)]
        public int? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
