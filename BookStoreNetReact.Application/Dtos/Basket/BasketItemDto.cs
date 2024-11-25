using BookStoreNetReact.Application.Dtos.Book;

namespace BookStoreNetReact.Application.Dtos.Basket
{
    public class BasketItemDto
    {
        public required int Id { get; set; }
        public required int Quantity { get; set; }
        public required BookDto Book { get; set; }
    }
}
