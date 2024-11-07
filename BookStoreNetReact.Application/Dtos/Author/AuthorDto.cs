namespace BookStoreNetReact.Application.Dtos.Author
{
    public class AuthorDto
    {
        public required int Id { get; set; }
        public required string FullName { get; set; }
        public required string Country { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
