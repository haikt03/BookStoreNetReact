using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        [StringLength(32, MinimumLength = 32)]
        public string Code { get; set; } = Guid.NewGuid().ToString("N");
        [Range(0, double.MaxValue)]
        public double Subtotal { get; set; }
        [Range(0, int.MaxValue)]
        public int DeliveryFee { get; set; }
        [Range(0, double.MaxValue)]
        public double Amount { get; set; }
        public string Note { get; set; } = "";
        public required string PaymentIntentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.PendingConfirmation;
        public required ShippingAddress ShippingAddress { get; set; }
        [Range(1, int.MaxValue)]
        public required int UserId { get; set; }
        public AppUser? User { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
