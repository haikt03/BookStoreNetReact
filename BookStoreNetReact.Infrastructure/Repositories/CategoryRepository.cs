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

        public IQueryable<Category> GetAll(FilterCategoryDto filterCategoryDto)
        {
            var categories = _context.Categories
                .Search(filterCategoryDto.Search)
                .Filter(filterCategoryDto.PId)
                .Sort(filterCategoryDto.Sort)
                .Include(c => c.PCategory);
            return categories;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(b => b.PCategory)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public IQueryable<Book> GetAllBooks(FilterBookDto filterBookDto, int categoryId)
        {
            var books = _context.Categories
                .Where(c => c.Id == categoryId && c.Books != null)
                .SelectMany(c => c.Books!)
                .Search(filterBookDto.Search)
                .Filter
                (
                    publishers: filterBookDto.Publishers,
                    languages: filterBookDto.Languages,
                    minPrice: filterBookDto.MinPrice,
                    maxPrice: filterBookDto.MaxPrice
                )
                .Sort(filterBookDto.Sort);
            return books;
        }
    }
}
