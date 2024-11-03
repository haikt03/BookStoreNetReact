using BookStoreNetReact.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreNetReact.Infrastructure.Extensions
{
    public static class PagingExension
    {
        public static async Task<PagedList<TDto>> ToPagedListAsync<TEntity, TDto>
        (
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, TDto>> selector,
            int pageSize,
            int pageIndex
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
            } catch (Exception ex)
            {
                Console.WriteLine($"{nameof(ToPagedListAsync)} failed");
                throw new Exception(ex.ToString());
            }
        }
    }
}
