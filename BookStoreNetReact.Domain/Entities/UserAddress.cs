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
        [StringLength(100, MinimumLength = 1)]
        public required string SpecificAddress { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public int? UserId { get; set; }
    }
}
