using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorDto>?> GetAllAuthorsAsync(FilterAuthorDto filterDto);
        Task<AuthorDetailDto?> GetAuthorByIdAsync(int authorId);
        Task<AuthorDetailDto?> CreateAuthorAsync(CreateAuthorDto createDto);
        Task<bool> UpdateAuthorAsync(UpdateAuthorDto updateDto, int authorId);
        Task<bool> DeleteAuthorAsync(int authorId);
        Task<AuthorFilterDto?> GetFilterAsync();
        Task<PagedList<BookDto>?> GetAllBooksByAuthorAsync(FilterBookDto filterDto, int authorId);
    }
}
