using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Book> GetAll(FilterBookDto filterBookDto)
        {
            var books = _context.Books
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

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<string>> GetAllPublishersAsync()
        {
            return await _context.Books
                .Select(b => b.Publisher)
                .Where(p => !string.IsNullOrEmpty(p))
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<string>> GetAllLanguagesAsync()
        {
            return await _context.Books
                .Select(b => b.Language)
                .Where(l => !string.IsNullOrEmpty(l))
                .Distinct()
                .ToListAsync();
        }
    }
}
