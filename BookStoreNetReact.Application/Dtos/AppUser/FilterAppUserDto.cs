using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class FilterAppUserDto : PaginationDto
    {
        public string? Search { get; set; } = "";
        public string? Sort { get; set; } = "";
    }
}
