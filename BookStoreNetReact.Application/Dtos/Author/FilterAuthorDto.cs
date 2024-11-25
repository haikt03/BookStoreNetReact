using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.Author
{
    public class FilterAuthorDto : FilterDto
    {
        public string? Countries { get; set; } = "";
    }
}
