using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class UpdateBookDto : CreateBookDto
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public required int Id { get; set; }
    }
}
