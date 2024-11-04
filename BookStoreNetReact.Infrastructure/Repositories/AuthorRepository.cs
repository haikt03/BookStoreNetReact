using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Author> GetAll(FilterAuthorDto filterAuthorDto)
        {
            var authors = _context.Authors
                .Search(filterAuthorDto.Search)
                .Filter(filterAuthorDto.Countries)
                .Sort(filterAuthorDto.Sort);
            return authors;
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<List<string>> GetAllCountriesAsync()
        {
            return await _context.Authors
                .Select(a => a.Country)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToListAsync();
        }

        public IQueryable<Book> GetAllBooks(FilterBookDto filterBookDto, int authorId)
        {
            var books = _context.Authors
                .Where(a => a.Id == authorId && a.Books != null)
                .SelectMany(a => a.Books!)
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
