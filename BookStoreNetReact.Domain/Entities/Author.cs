namespace BookStoreNetReact.Domain.Entities
{
    public class Author : BaseEntity
    {
        public required string FullName { get; set; }
        public required string Biography { get; set; }
        public required string Country { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
