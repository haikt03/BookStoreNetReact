using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities
{
    public class Category : BaseEntity
    {
        [StringLength(50, MinimumLength = 6)]
        public required string Name { get; set; }
        [Range(1, int.MaxValue)]
        public int? PId { get; set; }
        public Category? PCategory { get; set; }
        public List<Category>? CCategories { get; set; }
        public List<Book>? Books { get; set; }
    }
}
