using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Api.Dtos.Author
{
    public class UpdateAuthorDto : CreateAuthorDto
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public required int Id { get; set; }
    }
}
