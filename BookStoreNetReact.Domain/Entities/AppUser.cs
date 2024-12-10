using BookStoreNetReact.Domain.Attributes;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [StringLength(100, MinimumLength = 6)]
        public required string FullName { get; set; }
        [DateOfBirthRange]
        public DateOnly DateOfBirth { get; set; }
        [StringLength(50, MinimumLength = 6)]
        public override required string? UserName { get => base.UserName; set => base.UserName = value; }
        [EmailAddress]
        public override required string? Email { get => base.Email; set => base.Email = value; }
        [Phone]
        public override required string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        [StringLength(6, MinimumLength = 6)]
        public string? PhoneNumberConfirmationCode { get; set; }
        public DateTime? PhoneNumberConfirmationCodeExpiresAt { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public UserAddress? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
