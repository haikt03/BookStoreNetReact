using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<PagedList<BookDto>?> GetAllWithFilterAsync(FilterBookDto filterDto);
        Task<BookDetailDto?> GetByIdAsync(int bookId);
        Task<BookDetailDto?> CreateAsync(CreateBookDto createDto);
        Task<BookDetailDto?> UpdateAsync(UpdateBookDto updateDto, int bookId);
        Task<bool> DeleteAsync(int bookId);
        Task<BookFilterDto?> GetFilterAsync();
    }
}
