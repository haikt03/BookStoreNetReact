namespace BookStoreNetReact.Application.Dtos.Basket
{
    public class UpdateQuantityDto
    {
        public required int BookId { get; set; }
        public required int Quantity { get; set; }
    }
}
