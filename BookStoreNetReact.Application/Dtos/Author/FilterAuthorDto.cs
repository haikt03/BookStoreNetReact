using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class FilterAuthorDto : PaginationDto
    {
        public string? Search { get; set; } = "";
        public string? Countries { get; set; } = "";
        public string? Sort { get; set; } = "";
    }
}
