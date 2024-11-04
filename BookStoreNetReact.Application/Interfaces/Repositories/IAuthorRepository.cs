using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        IQueryable<Author> GetAll(FilterAuthorDto filterAuthorDto);
        Task<Author?> GetByIdAsync(int id);
        Task<List<string>> GetAllCountriesAsync();
        IQueryable<Book> GetAllBooks(FilterBookDto filterBookDto, int authorId);
    }
}