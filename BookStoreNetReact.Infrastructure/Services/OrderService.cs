using AutoMapper;
using BookStoreNetReact.Application.Dtos.Order;
using BookStoreNetReact.Application.Helpers;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Domain.Entities.OrderAggregate;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Stripe;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class OrderService : GenericService<OrderService>, IOrderService
    {
        private readonly UserManager<AppUser> _userManager;
        public OrderService(IMapper mapper, ICloudUploadService cloudUploadService, IUnitOfWork unitOfWork, ILogger<OrderService> logger, UserManager<AppUser> userManager) : base(mapper, cloudUploadService, unitOfWork, logger)
        {
            _userManager = userManager;
        }

        public async Task<PagedList<OrderDto>?> GetAllWithFilterAsync(FilterOrderDto filterDto)
        {
            try
            {
                if (filterDto.OrderDateStart.HasValue 
                    && filterDto.OrderDateEnd.HasValue 
                    && filterDto.OrderDateStart > filterDto.OrderDateEnd
                )
                {
                    throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");
                }
                var orders = _unitOfWork.OrderRepository.GetAllWithFilter(filterDto);
                var ordersDto = await orders.ToPagedListAsync
                (
                    selector: o => _mapper.Map<OrderDto>(o),
                    pageSize: filterDto.PageSize,
                    pageIndex: filterDto.PageIndex,
                    logger: _logger
                );
                return ordersDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting orders");
                return null;
            }
        }

        public async Task<PagedList<OrderDto>?> GetAllWithFilterByUserIdAsync(FilterOrderDto filterDto, int userId)
        {
            try
            {
                var orders = _unitOfWork.OrderRepository.GetAllWithFilterByUserId(filterDto, userId);
                var ordersDto = await orders.ToPagedListAsync
                (
                    selector: o => _mapper.Map<OrderDto>(o),
                    pageSize: filterDto.PageSize,
                    pageIndex: filterDto.PageIndex,
                    logger: _logger
                );
                return ordersDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting orders by user id");
                return null;
            }
        }

        public async Task<OrderDetailDto?> GetByIdAsync(int orderId, int userId)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetDetailByIdAsync(orderId);
                if (order == null)
                    throw new NullReferenceException("Order not found");

                var user = await _unitOfWork.AppUserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new InvalidOperationException("User not found");

                var roles = await _userManager.GetRolesAsync(user);
                if (order.User?.Id != userId && !roles.Contains("Admin"))
                    throw new InvalidOperationException("User does not have permission to access this order");

                var orderDto = _mapper.Map<OrderDetailDto>(order);
                return orderDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting order by id");
                return null;
            }
        }

        public async Task<OrderDetailDto?> CreateAsync(CreateOrderDto createDto, int userId)
        {
            try
            {
                var basket = await _unitOfWork.BasketRepository.GetByUserIdAsync(userId);
                if (basket == null)
                    throw new NullReferenceException("Basket not found");

                var orderItems = new List<OrderItem>();
                var subtotal = 0.0;

                foreach (var item in basket.Items)
                {
                    var book = await _unitOfWork.BookRepository.GetByIdAsync(item.BookId);
                    if (book == null)
                        throw new NullReferenceException("Book not found");
                    book.QuantityInStock -= item.Quantity;
                    var orderItem = new OrderItem
                    {
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                    };
                    orderItems.Add(orderItem);
                    subtotal += book.Price * (1 - (book.Discount / 100.0)) * item.Quantity;
                }

                var deliveryFee = subtotal > 200000 ? 0 : 30000;
                var amount = subtotal + deliveryFee;

                var order = new Order
                {
                    UserId = userId,
                    ShippingAddress = createDto.ShippingAddress,
                    Subtotal = subtotal,
                    DeliveryFee = deliveryFee,
                    Amount = amount,
                    PaymentIntentId = basket.PaymentIntentId ?? "",
                    Items = orderItems
                };
                await _unitOfWork.OrderRepository.AddAsync(order);

                basket.Items.RemoveAll(i => true);
                basket.PaymentIntentId = null;
                basket.ClientSecret = null;

                var result = await _unitOfWork.CompleteAsync();
                if (!result)
                    throw new InvalidOperationException("Create order failed");
                return _mapper.Map<OrderDetailDto>(order);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while creating order");
                return null;
            }
        }

        public async Task<bool> UpdatePaymentStatusAsync(Charge charge)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetFirstOrDefaultAsync(o => o.PaymentIntentId == charge.PaymentIntentId);
                if (order == null)
                    throw new NullReferenceException("Order not found");

                if (charge.Status == "succeeded") order.PaymentStatus = PaymentStatus.Completed;
                var result = await _unitOfWork.CompleteAsync();
                if (!result)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating payment status");
                return false;
            }
        }

        public async Task<OrderDetailDto?> UpdateOrderStatusAsync(UpdateOrderStatusDto updateDto, int orderId)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetDetailByIdAsync(orderId);
                if (order == null)
                    throw new NullReferenceException("Order not found");

                order.Note = updateDto.Note;
                if (Enum.TryParse(updateDto.OrderStatus, true, out OrderStatus parsedStatus))
                    order.OrderStatus = parsedStatus;
                else
                    order.OrderStatus = OrderStatus.PendingConfirmation;
                var result = await _unitOfWork.CompleteAsync();
                if (!result)
                    return null;
                return _mapper.Map<OrderDetailDto>(order);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while updating order status");
                return null;
            }
        }

        public async Task<OrderFilterDto?> GetFilterAsync()
        {
            try
            {
                var filterDto = await _unitOfWork.OrderRepository.GetFilterAsync();
                return filterDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while getting order filter");
                return null;
            }
        }
    }
}
