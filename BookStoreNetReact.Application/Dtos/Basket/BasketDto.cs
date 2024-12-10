namespace BookStoreNetReact.Application.Dtos.Basket
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string PaymentIntentId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
