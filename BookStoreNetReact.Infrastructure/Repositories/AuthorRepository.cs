using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Extensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {

        private readonly DbConnection _dbConnection;
        public AuthorRepository(AppDbContext context) : base(context)
        {
            _dbConnection = context.Database.GetDbConnection();
        }

        public IQueryable<Author> GetAllWithFilter(FilterAuthorDto filterDto)
        {
            var authors = _context.Authors
                .Search(filterDto.FullNameSearch)
                .Filter(filterDto.Countries)
                .Sort(filterDto.Sort);
            return authors;
        }

        public IQueryable<Author> GetAll()
        {
            var authors = _context.Authors;
            return authors;
        }

        public async Task<AuthorFilterDto> GetFilterAsync()
        {
            //var contries = await _context.Authors
            //    .Select(a => a.Country)
            //    .Where(c => !string.IsNullOrEmpty(c))
            //    .Distinct()
            //    .ToListAsync();
            var query = "SELECT DISTINCT Country " +
                        "FROM Authors " +
                        "WHERE Country IS NOT NULL AND Country <> ''";
            var countries = (await _dbConnection.QueryAsync<string>(query)).ToList();
            return new AuthorFilterDto { Countries = countries };
        }
    }
}
