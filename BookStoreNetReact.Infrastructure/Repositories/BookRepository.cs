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

        public IQueryable<Book> GetAll(FilterBookDto filterDto)
        {
            var books = _context.Books
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

        public async Task<Book?> GetByIdAsync(int bookId)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(a => a.Id == bookId);
            return book;
        }

        public async Task<List<string>> GetAllPublishersAsync()
        {
            var publishers = await _context.Books
                .Select(b => b.Publisher)
                .Where(p => !string.IsNullOrEmpty(p))
                .Distinct()
                .ToListAsync();
            return publishers;
        }

        public async Task<List<string>> GetAllLanguagesAsync()
        {
            var languages = await _context.Books
                .Select(b => b.Language)
                .Where(l => !string.IsNullOrEmpty(l))
                .Distinct()
                .ToListAsync();
            return languages;
        }
    }
}
