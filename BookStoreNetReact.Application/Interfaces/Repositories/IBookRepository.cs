using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
    }
}
