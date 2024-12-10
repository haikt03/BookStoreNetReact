using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Domain.Entities.OrderAggregate;

namespace BookStoreNetReact.Application.Dtos.Order
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public double Subtotal { get; set; }
        public int DeliveryFee { get; set; }
        public double Amount { get; set; }
        public string PaymentIntentId { get; set; } = "";
        public string PaymentStatus { get; set; } = "";
        public string OrderStatus { get; set; } = "";
        public DateTime OrderDate { get; set; }
        public required ShippingAddress ShippingAddress { get; set; }
        public AppUserDto User { get; set; } = new AppUserDto();
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
