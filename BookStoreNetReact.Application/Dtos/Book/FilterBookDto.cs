using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class FilterBookDto : PaginationDto
    {
        public string? Search { get; set; } = "";
        public string? Publishers { get; set; } = "";
        public string? Languages { get; set; } = "";
        public int? MinPrice { get; set; } = 0;
        public int? MaxPrice { get; set; } = 0;
        public string? Sort { get; set; } = "";
    }
}
