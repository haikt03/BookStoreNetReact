using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        IQueryable<Author> GetAll(FilterAuthorDto filterDto);
        Task<Author?> GetByIdAsync(int authorId);
        Task<List<string>> GetAllCountriesAsync();
        IQueryable<Book> GetAllBooks(FilterBookDto filterDto, int authorId);
    }
}