using BookStoreNetReact.Domain.Entities.OrderAggregate;

namespace BookStoreNetReact.Application.Dtos.Order
{
    public class CreateOrderDto
    {
        public required ShippingAddress ShippingAddress { get; set; }
    }
}
