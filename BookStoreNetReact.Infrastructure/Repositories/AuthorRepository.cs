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

        public IQueryable<Author> GetAll(FilterAuthorDto filterDto)
        {
            var authors = _context.Authors
                .Search(filterDto.Search)
                .Filter(filterDto.Countries)
                .Sort(filterDto.Sort);
            return authors;
        }

        public async Task<Author?> GetByIdAsync(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            return author;
        }

        public IQueryable<Book> GetAllBooks(FilterBookDto filterDto, int authorId)
        {
            var books = _context.Authors
                .Where(a => a.Id == authorId && a.Books != null)
                .SelectMany(a => a.Books!)
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

        public async Task<AuthorFilterDto> GetFilterAsync()
        {
            var contries = await _context.Authors
                .Select(a => a.Country)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToListAsync();
            return new AuthorFilterDto { Countries = contries };
        }
    }
}
