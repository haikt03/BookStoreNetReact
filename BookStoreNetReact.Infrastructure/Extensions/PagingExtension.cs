using BookStoreNetReact.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class PagingExtension
    {
        public static async Task<PagedList<TDto>?> ToPagedListAsync<TEntity, TDto>
        (
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, TDto>> selector,
            int pageSize,
            int pageIndex,
            ILogger logger
        )
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                    throw new ArgumentOutOfRangeException("Paging parameters are out of range");

                var totalCount = await query.CountAsync();
                var items = await query.Skip((pageIndex - 1) * pageSize)
                                       .Take(pageSize)
                                       .Select(selector)
                                       .ToListAsync();
                return new PagedList<TDto>(items, totalCount, pageSize, pageIndex);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred while paging");
                return null;
            }
        }
    }
}
