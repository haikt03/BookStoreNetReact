using BookStoreNetReact.Application.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class PagingExension
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageSize, int pageIndex)
        {
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedList<T>(items, totalCount, pageSize, pageIndex);
        }
    }
}
