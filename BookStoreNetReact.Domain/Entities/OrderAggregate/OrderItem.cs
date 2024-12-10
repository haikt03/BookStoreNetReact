using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        [Range(0, int.MaxValue)]
        public required int Quantity { get; set; }
        [Range(1, int.MaxValue)]
        public required int BookId { get; set; }
        public Book? Book { get; set; }
        [Range(1, int.MaxValue)]
        public int? OrderId { get; set; }
    }
}
