using BookStoreNetReact.Application.Dtos.Order;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using BookStoreNetReact.Domain.Entities.OrderAggregate;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> GetAll(FilterOrderDto filterDto)
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .Search(filterDto.CodeSearch, filterDto.UserSearch)
                .Filter
                (
                    filterDto.OrderStatuses,
                    filterDto.PaymentStatuses,
                    filterDto.MinAmount,
                    filterDto.MaxAmount,
                    filterDto.OrderDateStart,
                    filterDto.OrderDateEnd
                )
                .Sort(filterDto.Sort);
            return orders;
        }

        public IQueryable<Order> GetAllByUserId(FilterOrderDto filterDto, int userId)
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .Search(filterDto.CodeSearch, filterDto.UserSearch)
                .Filter
                (
                    filterDto.OrderStatuses,
                    filterDto.PaymentStatuses,
                    filterDto.MinAmount,
                    filterDto.MaxAmount,
                    filterDto.OrderDateStart,
                    filterDto.OrderDateEnd
                )
                .Sort(filterDto.Sort);
            return orders;
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            var orders = await _context.Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.User)
                .Include(o => o.Items).ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            return orders;
        }

        public async Task<Order?> GetByPaymentIntentIdAsync(string paymentIntentId)
        {
            var orders = await _context.Orders
                .FirstOrDefaultAsync(o => o.PaymentIntentId
                == paymentIntentId);
            return orders;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<OrderFilterDto> GetFilterAsync()
        {
            var query = await _context.Orders
                .Select(o => new { o.OrderStatus, o.PaymentStatus, o.Amount })
                .ToListAsync();

            var orderStatuses = Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(os => os.ToString())
                .ToList();

            var paymentStatuses = Enum.GetValues(typeof(PaymentStatus))
                .Cast<PaymentStatus>()
                .Select(ps => ps.ToString())
                .ToList();

            var minAmount = query.Any() ? query.Min(o => o.Amount) : 0;
            var maxAmount = query.Any() ? query.Max(o => o.Amount) : 0;

            return new OrderFilterDto
            {
                OrderStatuses = orderStatuses,
                PaymentStatuses = paymentStatuses,
                MaxAmount = maxAmount,
                MinAmount = minAmount
            };
        }
    }
}
