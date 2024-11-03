using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Extensions;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Category> GetAllAsync(FilterCategoryDto filterCategoryDto)
        {
            var categories = _context.Categories
                .Search(filterCategoryDto.Search)
                .Sort(filterCategoryDto.Sort);
            return categories;
        }
    }
}
