using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public BasketRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Basket?> GetByUserIdAsync(int userId)
        {
            var basket = await _context.Baskets
                .Include(b => b.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(b => b.UserId == userId);
            return basket;
        }
    }
}
