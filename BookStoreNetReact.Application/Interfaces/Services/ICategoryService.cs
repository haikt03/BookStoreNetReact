using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<PagedList<CategoryDto>> GetAllCategories(FilterCategoryDto filterCategoryDto);
        Task<CategoryDto> GetCategoryById(int categoryId);
        Task<bool> CreateCategory(CreateCategoryDto createCategoryDto);
        Task<bool> UpdateCategory(UpdateCategoryDto updateCategoryDto);
        Task<bool> DeleteCategory(int categoryId);
    }
}
