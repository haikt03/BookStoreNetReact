using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Extensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly DbConnection _dbConnection;
        public BookRepository(AppDbContext context) : base(context)
        {
            _dbConnection = context.Database.GetDbConnection();
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
            //var minPrice = await _context.Books.MinAsync(b => b.Price);
            //var maxPrice = await _context.Books.MaxAsync(b => b.Price);
            var query = "SELECT MIN(Price) AS minPrice, MAX(Price) AS MaxPrice FROM Books";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<(int, int)>(query);
            return result;
        }
    }
}
