namespace BookStoreNetReact.Application.Helpers
{
    public class PagedList<T> : List<T>
    {
        public Pagination Pagination { get; set; }

        public PagedList(List<T> items, int totalCount, int pageSize, int pageIndex)
        {
            Pagination = new Pagination
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            AddRange(items);
        }
    }
}
