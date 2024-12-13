using BookStoreNetReact.Domain.Entities.OrderAggregate;

namespace BookStoreNetReact.Application.Dtos.Order
{
    public class UpdateOrderStatusDto
    {
        public string OrderStatus { get; set; } = "PendingConfirmation";
        public string Note { get; set; } = "";
    }
}
