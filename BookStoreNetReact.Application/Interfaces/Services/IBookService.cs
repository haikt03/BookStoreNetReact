using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<PagedList<BookDto>?> GetAllBooksAsync(FilterBookDto filterDto);
        Task<DetailBookDto?> GetBookByIdAsync(int bookId);
        Task<DetailBookDto?> CreateBookAsync(CreateBookDto createDto);
        Task<bool> UpdateBookAsync(UpdateBookDto updateDto, int bookId);
        Task<bool> DeleteBookAsync(int bookId);
        Task<List<string>?> GetAllPublishersOfBooksAsync();
        Task<List<string>?> GetAllLanguagesOfBooksAsync();
    }
}
