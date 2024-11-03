using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorDto>> GetAllAuthorsAsync(FilterAuthorDto filterAuthorDto);
        Task<AuthorDto> GetAuthorByIdAsync(int authorId);
        Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto);
        Task<bool> UpdateAuthorAsync(UpdateAuthorDto updateAuthorDto, int id);
        Task<bool> DeleteAuthorAsync(int authorId);
        Task<List<string>> GetAllCountriesOfAuthorsAsync();
    }
}
