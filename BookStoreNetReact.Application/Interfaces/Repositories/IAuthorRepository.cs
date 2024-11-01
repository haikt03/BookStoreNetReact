using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<List<Author>> GetAllAsync(FilterAuthorDto filterAuthorDto);
    }
}
