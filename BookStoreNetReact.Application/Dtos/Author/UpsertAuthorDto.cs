using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class UpsertAuthorDto
    {
        public required string FullName { get; set; }
        public required string Biography { get; set; }
        public required string Country { get; set; }
    }
}
