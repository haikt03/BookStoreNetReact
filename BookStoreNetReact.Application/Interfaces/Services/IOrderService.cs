using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Order;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Domain.Entities.OrderAggregate;
using Stripe;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<PagedList<OrderDto>?> GetAllOrdersAsync(FilterOrderDto filterDto);
        Task<PagedList<OrderDto>?> GetAllOrdersByUserIdAsync(FilterOrderDto filterDto, int userId);
        Task<OrderDetailDto?> GetOrderByIdAsync(int orderId, int userId);
        Task<OrderDetailDto?> CreateOrderAsync(CreateOrderDto createDto, int userId);
        Task<bool> UpdateOrderStatusAsync(Charge charge);
        Task<OrderFilterDto?> GetFilterAsync();
    }
}
