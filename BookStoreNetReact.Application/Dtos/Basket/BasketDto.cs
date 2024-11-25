namespace BookStoreNetReact.Application.Dtos.Basket
{
    public class BasketDto
    {
        public required int Id { get; set; }
        public required List<BasketItemDto> Items { get; set; }
    }
}
