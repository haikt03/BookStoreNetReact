using Microsoft.AspNetCore.Http;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class UpdateAuthorDto
    {
        public string? FullName { get; set; }
        public string? Biography { get; set; }
        public string? Country { get; set; }
        public IFormFile? File { get; set; }
    }
}
