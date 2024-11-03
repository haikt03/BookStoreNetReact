using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<PagedList<CategoryDto>> GetAllCategoriesAsync(FilterCategoryDto filterCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
