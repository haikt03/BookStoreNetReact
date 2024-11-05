using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Category> GetAll(FilterCategoryDto filterDto)
        {
            var categories = _context.Categories
                .Search(filterDto.Search)
                .Filter(filterDto.PId)
                .Sort(filterDto.Sort)
                .Include(c => c.PCategory);
            return categories;
        }

        public async Task<Category?> GetByIdAsync(int categoryId)
        {
            var category = await _context.Categories
                .Include(b => b.PCategory)
                .FirstOrDefaultAsync(a => a.Id == categoryId);
            return category;
        }

        public IQueryable<Book> GetAllBooks(FilterBookDto filterDto, int categoryId)
        {
            var books = _context.Categories
                .Where(c => c.Id == categoryId && c.Books != null)
                .SelectMany(c => c.Books!)
                .Search(filterDto.Search)
                .Filter
                (
                    publishers: filterDto.Publishers,
                    languages: filterDto.Languages,
                    minPrice: filterDto.MinPrice,
                    maxPrice: filterDto.MaxPrice
                )
                .Sort(filterDto.Sort);
            return books;
        }
    }
}
