using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        IQueryable<Author> GetAllWithFilter(FilterAuthorDto filterDto);
        IQueryable<Author> GetAll();
    }
}