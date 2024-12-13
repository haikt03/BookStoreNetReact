using BookStoreNetReact.Application.Dtos.Order;
using BookStoreNetReact.Domain.Entities.OrderAggregate;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        IQueryable<Order> GetAllWithFilter(FilterOrderDto filterDto);
        IQueryable<Order> GetAllWithFilterByUserId(FilterOrderDto filterDto, int userId);
        Task<Order?> GetDetailByIdAsync(int orderId);
        Task<OrderFilterDto> GetFilterAsync();
    }
}
