using BookStoreNetReact.Application.Dtos.Order;
using BookStoreNetReact.Application.Helpers;
using Stripe;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<PagedList<OrderDto>?> GetAllWithFilterAsync(FilterOrderDto filterDto);
        Task<PagedList<OrderDto>?> GetAllWithFilterByUserIdAsync(FilterOrderDto filterDto, int userId);
        Task<OrderDetailDto?> GetByIdAsync(int orderId, int userId);
        Task<OrderDetailDto?> CreateAsync(CreateOrderDto createDto, int userId);
        Task<bool> UpdatePaymentStatusAsync(Charge charge);
        Task<OrderDetailDto?> UpdateOrderStatusAsync(UpdateOrderStatusDto updateDto, int orderId);
        Task<OrderFilterDto?> GetFilterAsync();
    }
}
