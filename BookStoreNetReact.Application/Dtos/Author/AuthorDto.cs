namespace BookStoreNetReact.Application.Dtos.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Country { get; set; } = "";
        public string PublicId { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
