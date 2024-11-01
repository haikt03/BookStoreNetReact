using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorDto>> GetAllAuthors(FilterAuthorDto filterAuthorDto);
        Task<AuthorDto> GetAuthorById(int authorId);
        Task<bool> CreateAuthor(CreateAuthorDto createAuthorDto);
        Task<bool> UpdateAuthor(UpdateAuthorDto updateAuthorDto);
        Task<bool> DeleteAuthor(int authorId);
        Task<List<string>> GetAllCountriesOfAuthor();
    }
}
