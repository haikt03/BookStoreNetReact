using BookStoreNetReact.Application.Dtos.Book;

namespace BookStoreNetReact.Application.Dtos.Order
{
    public class OrderItemDto
    {
        public required int Id { get; set; }
        public required int Quantity { get; set; }
        public required BookDto Book { get; set; }
    }
}
