using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly AppDbContext _context;
        public BasketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Basket?> GetByUserIdAsync(int userId)
        {
            var basket = await _context.Baskets
                .Include(b => b.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(b => b.UserId == userId);
            return basket;
        }

        public async Task AddAsync(Basket basket)
        {
            await _context.AddAsync(basket);
        }

        public void Update(Basket basket)
        {
            _context.Baskets.Update(basket);
        }

        public void Clear(Basket basket)
        {
            _context.BasketItems.RemoveRange(basket.Items);
            basket.PaymentIntentId = null;
            basket.ClientSecret = null;
        }
    }
}
