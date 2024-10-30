namespace BookStoreNetReact.Domain.Entities
{
    public class Book : BaseEntity
    {
        public required string Name { get; set; }
        public string? Translator { get; set; }
        public required string Publisher { get; set; }
        public required int PublishedYear { get; set; }
        public required string Language { get; set; }
        public required int Weight { get; set; }
        public required int NumberOfPages { get; set; }
        public required string Form { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
        public required int Discount { get; set; }
        public required int QuantityInStock { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
