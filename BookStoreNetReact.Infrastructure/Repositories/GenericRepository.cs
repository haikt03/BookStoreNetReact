using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id, string? includeProperties = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = GetQueryWithIncludedProperties(query, includeProperties);
            }
            var result = await query.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> criteria, string? includeProperties = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = GetQueryWithIncludedProperties(query, includeProperties);
            }
            var result = await query.Where(criteria).FirstOrDefaultAsync();
            return result;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public Task<List<string>> GetStringFilterAsync(Expression<Func<T, string>> selector)
        {
            var result = _context.Set<T>()
                .Select(selector)
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct()
                .ToListAsync();
            return result;
        }

        public static IQueryable<T> GetQueryWithIncludedProperties(IQueryable<T> query, string includeProperties)
        {
            var props = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var prop in props)
            {
                query = query.Include(prop);
            }
            return query;
        }
    }
}
