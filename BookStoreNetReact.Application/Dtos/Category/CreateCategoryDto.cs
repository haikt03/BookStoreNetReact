using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class CreateCategoryDto : BaseDto
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public required string Name { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = RequiredErrorMessage)]
        public required int PId { get; set; }
    }
}
