using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class FilterCategoryDto : PaginationDto
    {
        public string? Search { get; set; } = "";
        public int? PId = 0;
        public string? Sort { get; set; } = "";
    }
}
