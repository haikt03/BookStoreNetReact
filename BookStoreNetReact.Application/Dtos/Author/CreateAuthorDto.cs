using Microsoft.AspNetCore.Http;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class CreateAuthorDto
    {
        public required string FullName { get; set; }
        public required string Biography { get; set; }
        public required string Country { get; set; }
        public IFormFile? File { get; set; }
    }
}
