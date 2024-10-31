namespace BookStoreNetReact.Application.Helpers
{
    public class Pagination
    {
        public required int PageSize { get; set; }
        public required int PageIndex { get; set; }
        public required int TotalCount { get; set; }
        public required int TotalPages { get; set; }
    }
}
