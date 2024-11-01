using BookStoreNetReact.Application.Dtos.Image;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class UpdateBookDto
    {
        public required int Id { get; set; }
        public string? Name { get; set; }
        public string? Translator { get; set; }
        public string? Publisher { get; set; }
        public int? PublishedYear { get; set; }
        public string? Language { get; set; }
        public int? Weight { get; set; }
        public int? NumberOfPages { get; set; }
        public string? Form { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public int? QuantityInStock { get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public UploadImageDto? Image { get; set; }
    }
}
