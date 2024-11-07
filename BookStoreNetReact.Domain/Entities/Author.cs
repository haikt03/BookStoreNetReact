using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities
{
    public class Author : BaseEntity
    {
        [StringLength(100, MinimumLength = 1)]
        public required string FullName { get; set; }
        [StringLength(500, MinimumLength = 50)]
        public required string Biography { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public required string Country { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public List<Book>? Books { get; set; }
    }
}
