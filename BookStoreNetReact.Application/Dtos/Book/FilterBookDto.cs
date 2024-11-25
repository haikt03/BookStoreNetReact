using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.Book
{
    public class FilterBookDto : FilterDto
    {
        public string? Publishers { get; set; } = "";
        public string? Languages { get; set; } = "";
        public string? Categories { get; set; } = "";
        public string? AuthorSearch { get; set; } = "";
        public int? MinPrice { get; set; } = 0;
        public int? MaxPrice { get; set; } = 0;
    }
}
