using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<PagedList<CategoryDto>> GetAllCategoriesAsync(FilterCategoryDto filterCategoryDto);
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<bool> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
