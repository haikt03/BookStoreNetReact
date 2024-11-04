using BookStoreNetReact.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class PagingExension
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
                if (pageIndex < 1)
                    throw new ArgumentOutOfRangeException(nameof(pageIndex));
                if (pageSize < 1)
                    throw new ArgumentOutOfRangeException(nameof(pageSize));

                var totalCount = await query.CountAsync();
                var items = await query.Skip((pageIndex - 1) * pageSize)
                                       .Take(pageSize)
                                       .Select(selector)
                                       .ToListAsync();
                return new PagedList<TDto>(items, totalCount, pageSize, pageIndex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                logger.LogWarning(ex, "Paging parameters are out of range");
                return null;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred while paging");
                return null;
            }
        }
    }
}
