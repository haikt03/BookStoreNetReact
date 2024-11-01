using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public Task<List<Category>> GetAllAsync(FilterCategoryDto filterCategoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
