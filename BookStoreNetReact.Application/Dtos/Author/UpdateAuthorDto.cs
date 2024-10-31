using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class UpdateAuthorDto : CreateAuthorDto
    {
        public required int Id { get; set; }
    }
}
