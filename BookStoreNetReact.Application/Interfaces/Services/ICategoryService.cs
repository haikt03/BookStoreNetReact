using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<PagedList<CategoryDto>?> GetAllCategoriesAsync(FilterCategoryDto filterDto);
        Task<CategoryDetailDto?> GetCategoryByIdAsync(int categoryId);
        Task<CategoryDetailDto?> CreateCategoryAsync(CreateCategoryDto createDto);
        Task<bool> UpdateCategoryAsync(UpdateCategoryDto updateDto, int categoryId);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<PagedList<BookDto>?> GetAllBooksByCategoryAsync(FilterBookDto filterDto, int categoryId);
    }
}
