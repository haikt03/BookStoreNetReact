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

        public IQueryable<Book> GetAllWithFilter(FilterBookDto filterDto)
        {
            var books = _context.Books
                .Search(filterDto.NameSearch, filterDto.AuthorSearch)
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

        public async Task<(int, int)> GetPriceRangeAsync()
        {
            var minPrice = await _context.Books.MinAsync(b => b.Price);
            var maxPrice = await _context.Books.MaxAsync(b => b.Price);
            return (minPrice, maxPrice);
        }
    }
}
