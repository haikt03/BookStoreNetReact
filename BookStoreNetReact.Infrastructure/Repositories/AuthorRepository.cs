using BookStoreNetReact.Application.Dtos.Author;
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

        public async Task<List<Author>> GetAllAsync(FilterAuthorDto filterAuthorDto)
        {
            var authors = await _context.Authors
                .AsQueryable()
                .Search(filterAuthorDto.Search)
                .Filter(filterAuthorDto.Countries)
                .Sort(filterAuthorDto.Sort)
                .ToListAsync();
            return authors;
        }
    }
}
