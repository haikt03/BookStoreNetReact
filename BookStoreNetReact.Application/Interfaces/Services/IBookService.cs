using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<PagedList<BookDto>> GetAllBooks(FilterBookDto filterBookDto);
        Task<BookDto> GetBookById(int bookId);
        Task<bool> CreateBook(CreateBookDto createBookDto);
        Task<bool> UpdateBook(UpdateBookDto updateBookDto);
        Task<bool> DeleteBook(int bookId);
        Task<List<string>> GetAllAuthorsOfBook();
        Task<List<string>> GetAllCategoriesOfBook();
        Task<List<string>> GetAllLanguagesOfBook();
    }
}
