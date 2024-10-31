using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class UpdateCategoryDto : CreateCategoryDto
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public required int Id { get; set; }
    }
}
