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
                .Search(filterDto.Search, filterDto.AuthorSearch)
                .Filter
                (
                    publishers: filterDto.Publishers,
                    languages: filterDto.Languages,
                    categories: filterDto.Categories,
                    minPrice: filterDto.MinPrice,
                    maxPrice: filterDto.MaxPrice
                )
                .Sort(filterDto.Sort);
            return books;
        }

        public async Task<Book?> GetByIdAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            return book;
        }

        public async Task<Book?> GetDetailByIdAsync(int bookId)
        {
            var book = await _context.Books
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

        public async Task<BookFilterDto> GetFilterAsync()
        {
            var publishers = await _context.Books
                .Select(b => b.Publisher)
                .Where(p => !string.IsNullOrEmpty(p))
                .Distinct()
                .ToListAsync();

            var languages = await _context.Books
                .Select(b => b.Language)
                .Where(l => !string.IsNullOrEmpty(l))
                .Distinct()
                .ToListAsync();

            var categories = await _context.Books
                .Select(b => b.Category)
                .Where(l => !string.IsNullOrEmpty(l))
                .Distinct()
                .ToListAsync();

            var minPrice = await _context.Books.MinAsync(b => b.Price);
            var maxPrice = await _context.Books.MaxAsync(b => b.Price);

            return new BookFilterDto
            {
                Publishers = publishers,
                Languages = languages,
                Categories = categories,
                MinPrice = (int)(Math.Floor(minPrice / 100000.0) * 100000),
                MaxPrice = (int)(Math.Ceiling(maxPrice / 100000.0) * 100000)
            };
        }
    }
}
