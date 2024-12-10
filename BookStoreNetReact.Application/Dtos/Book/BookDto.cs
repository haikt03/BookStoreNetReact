namespace BookStoreNetReact.Application.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public int Price { get; set; }
        public int Discount { get; set; }
        public int QuantityInStock { get; set; }
        public string PublicId { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
