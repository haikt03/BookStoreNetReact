using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<PagedList<BookDto>?> GetAllBooksAsync(FilterBookDto filterDto);
        Task<BookDetailDto?> GetBookByIdAsync(int bookId);
        Task<BookDetailDto?> CreateBookAsync(CreateBookDto createDto);
        Task<BookDetailDto?> UpdateBookAsync(UpdateBookDto updateDto, int bookId);
        Task<bool> DeleteBookAsync(int bookId);
        Task<BookFilterDto?> GetFilterAsync();
    }
}
