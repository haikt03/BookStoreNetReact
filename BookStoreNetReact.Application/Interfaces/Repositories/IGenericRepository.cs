using BookStoreNetReact.Domain.Entities;
using System.Linq.Expressions;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id, string? includeProperties = null);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> criteria, string? includeProperties = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<List<string>> GetStringFilterAsync(Expression<Func<T, string>> selector);
    }
}
