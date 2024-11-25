using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public required string Token { get; set; }
        public required DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        [Range(1, int.MaxValue)]
        public required int UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
