using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IQueryable<Book> GetAll(FilterBookDto filterBookDto);
        Task<Book?> GetByIdAsync(int id);
        Task<List<string>> GetAllPublishersAsync();
        Task<List<string>> GetAllLanguagesAsync();
    }
}
