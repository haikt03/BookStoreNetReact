using BookStoreNetReact.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities
{
    public class Book : BaseEntity
    {
        [StringLength(250, MinimumLength = 6)]
        public required string Name { get; set; }
        [StringLength(100, MinimumLength = 1)]
        public string? Translator { get; set; }
        [StringLength(100, MinimumLength = 1)]
        public required string Publisher { get; set; }
        [PublishedYearRange]
        public required int PublishedYear { get; set; }
        [StringLength(50, MinimumLength = 6)]
        public required string Language { get; set; }
        [Range(1, int.MaxValue)]
        public required int Weight { get; set; }
        [Range(1, int.MaxValue)]
        public required int NumberOfPages { get; set; }
        [StringLength(50, MinimumLength = 6)]
        public required string Form { get; set; }
        [StringLength(500, MinimumLength = 50)]
        public required string Description { get; set; }
        [Range(1, int.MaxValue)]
        public required int Price { get; set; }
        [Range(0, 100)]
        public required int Discount { get; set; }
        [Range(0, int.MaxValue)]
        public required int QuantityInStock { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        [Range(1, int.MaxValue)]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        [Range(1, int.MaxValue)]
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
