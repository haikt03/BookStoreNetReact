using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class FilterCategoryDto : FilterDto
    {
        public int? PId { get; set; } = 0;
    }
}
