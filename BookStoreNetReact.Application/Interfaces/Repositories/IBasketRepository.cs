using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket?> GetByUserIdAsync(int userId);
        Task AddAsync(Basket basket);
        void Update(Basket basket);
        void Clear(Basket basket);
    }
}
