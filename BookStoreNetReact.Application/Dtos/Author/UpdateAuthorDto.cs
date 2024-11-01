using BookStoreNetReact.Application.Dtos.Image;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class UpdateAuthorDto
    {
        public required int Id { get; set; }
        public string? FullName { get; set; }
        public string? Biography { get; set; }
        public string? Country { get; set; }
        public UploadImageDto? Image { get; set; }
    }
}
