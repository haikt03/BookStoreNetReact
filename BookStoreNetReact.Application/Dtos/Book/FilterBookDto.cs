using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class FilterBookDto : PaginationDto
    {
        public string? Search { get; set; }
        public string? Categories { get; set; }
        public string? Authors { get; set; }
        public string? Languages { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Sort { get; set; }
    }
}
