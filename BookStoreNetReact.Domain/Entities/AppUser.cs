using Microsoft.AspNetCore.Identity;

namespace BookStoreNetReact.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public required string FullName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public UserAddress? Address { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
