namespace BookStoreNetReact.Application.Dtos.Book
{
    public class BookDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required int Discount { get; set; }
        public required int QuantityInStock { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
