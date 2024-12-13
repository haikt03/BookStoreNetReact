using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        Task<Basket?> GetByUserIdAsync(int userId);
    }
}
