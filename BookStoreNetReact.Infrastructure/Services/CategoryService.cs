using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Services;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<PagedList<CategoryDto>> GetAllCategories(FilterCategoryDto filterCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
