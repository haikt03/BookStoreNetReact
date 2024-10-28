namespace BookStoreNetReact.Domain.Entities
{
    public class Author : BaseEntity
    {
        public required string FullName { get; set; }
        public string? Biography { get; set; }
        public string? Country { get; set; }
        
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }

        public List<Book>? Books { get; set; }
    }
}
