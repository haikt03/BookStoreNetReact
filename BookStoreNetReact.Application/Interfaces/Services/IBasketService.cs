using BookStoreNetReact.Application.Dtos.Basket;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IBasketService
    {
        Task<BasketDto?> GetByUserIdAsync(int userId);
        Task<BasketDto?> UpdateQuantityAsync(UpdateQuantityDto updateDto, int userId, string type);
    }
}
