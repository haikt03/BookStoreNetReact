namespace BookStoreNetReact.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public int? UserId { get; set; }
        public List<BasketItem>? Items { get; set; }
    }
}
