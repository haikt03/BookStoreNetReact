using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
