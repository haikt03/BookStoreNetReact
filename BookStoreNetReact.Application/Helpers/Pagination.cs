namespace BookStoreNetReact.Application.Helpers
{
    public class Pagination
    {
        public required int PageSize { get; set; } = 6;
        public required int PageIndex { get; set; } = 1;
        public required int TotalCount { get; set; }
        public required int TotalPages { get; set; }
    }
}
