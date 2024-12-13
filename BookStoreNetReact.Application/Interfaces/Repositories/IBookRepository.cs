using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IQueryable<Book> GetAllWithFilter(FilterBookDto filterDto);
        Task<(int, int)> GetPriceRangeAsync();
    }
}
