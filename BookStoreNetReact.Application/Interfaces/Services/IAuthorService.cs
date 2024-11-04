using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorDto>?> GetAllAuthorsAsync(FilterAuthorDto filterAuthorDto);
        Task<DetailAuthorDto?> GetAuthorByIdAsync(int authorId);
        Task<DetailAuthorDto?> CreateAuthorAsync(CreateAuthorDto createAuthorDto);
        Task<bool> UpdateAuthorAsync(UpdateAuthorDto updateAuthorDto, int id);
        Task<bool> DeleteAuthorAsync(int authorId);
        Task<List<string>?> GetAllCountriesOfAuthorsAsync();
        Task<PagedList<BookDto>?> GetAllBooksByAuthorAsync(FilterBookDto filterBookDto, int authorId);
    }
}
