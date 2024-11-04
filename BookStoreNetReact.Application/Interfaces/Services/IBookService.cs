using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<PagedList<BookDto>?> GetAllBooksAsync(FilterBookDto filterBookDto);
        Task<DetailBookDto?> GetBookByIdAsync(int bookId);
        Task<DetailBookDto?> CreateBookAsync(CreateBookDto createBookDto);
        Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto, int bookId);
        Task<bool> DeleteBookAsync(int bookId);
        Task<List<string>?> GetAllPublishersOfBooksAsync();
        Task<List<string>?> GetAllLanguagesOfBooksAsync();
    }
}
