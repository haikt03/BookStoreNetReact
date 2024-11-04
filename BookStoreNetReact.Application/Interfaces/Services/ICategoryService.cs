using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Helpers;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<PagedList<CategoryDto>?> GetAllCategoriesAsync(FilterCategoryDto filterCategoryDto);
        Task<DetailCategoryDto?> GetCategoryByIdAsync(int categoryId);
        Task<DetailCategoryDto?> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<bool> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, int categoryId);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<PagedList<BookDto>?> GetAllBooksByCategoryAsync(FilterBookDto filterBookDto, int categoryId);
    }
}
