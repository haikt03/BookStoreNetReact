using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorDto>?> GetAllWithFilterAsync(FilterAuthorDto filterDto);
        Task<AuthorDetailDto?> GetByIdAsync(int authorId);
        Task<AuthorDetailDto?> CreateAsync(CreateAuthorDto createDto);
        Task<AuthorDetailDto?> UpdateAsync(UpdateAuthorDto updateDto, int authorId);
        Task<bool> DeleteAsync(int authorId);
        Task<AuthorFilterDto?> GetFilterAsync();
        Task<List<AuthorForUpsertBookDto>?> GetAllForUpsertBookAsync();
    }
}
