using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<PagedList<BookDto>> GetAllBooksAsync(FilterBookDto filterBookDto);
        Task<BookDto> GetBookByIdAsync(int bookId);
        Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);
        Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto);
        Task<bool> DeleteBookAsync(int bookId);
        Task<List<string>> GetAllPublishersOfBooksAsync();
        Task<List<string>> GetAllLanguagesOfBooksAsync();
    }
}
