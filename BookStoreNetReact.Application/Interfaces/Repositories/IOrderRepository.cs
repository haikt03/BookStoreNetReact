using BookStoreNetReact.Application.Dtos.Order;
using BookStoreNetReact.Domain.Entities.OrderAggregate;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll(FilterOrderDto filterDto);
        IQueryable<Order> GetAllByUserId(FilterOrderDto filterDto, int userId);
        Task<Order?> GetByIdAsync(int orderId);
        Task<Order?> GetByPaymentIntentIdAsync(string paymentIntentId);
        Task AddAsync(Order order);
        void Update(Order order);
        Task<OrderFilterDto> GetFilterAsync();
    }
}
