using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.Category
{
    public class FilterCategory : PaginationDto
    {
        public string? Search { get; set; }
        public string? Sort { get; set; }
    }
}
