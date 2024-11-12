namespace BookStoreNetReact.Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public required int Quantity { get; set; }
        public int? BookId { get; set; }
        public Book? Book { get; set; }
        public int? BasketId { get; set; }
        public Basket? Basket { get; set; }
    }
}
