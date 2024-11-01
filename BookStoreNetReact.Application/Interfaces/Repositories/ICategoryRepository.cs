using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<List<Category>> GetAllAsync(FilterCategoryDto filterCategoryDto);
    }
}
