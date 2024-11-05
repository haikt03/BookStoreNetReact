using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorDto>?> GetAllAuthorsAsync(FilterAuthorDto filterDto);
        Task<DetailAuthorDto?> GetAuthorByIdAsync(int authorId);
        Task<DetailAuthorDto?> CreateAuthorAsync(CreateAuthorDto createDto);
        Task<bool> UpdateAuthorAsync(UpdateAuthorDto updateDto, int authorId);
        Task<bool> DeleteAuthorAsync(int authorId);
        Task<List<string>?> GetAllCountriesOfAuthorsAsync();
        Task<PagedList<BookDto>?> GetAllBooksByAuthorAsync(FilterBookDto filterDto, int authorId);
    }
}
