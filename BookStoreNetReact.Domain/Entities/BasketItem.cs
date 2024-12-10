using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(1, int.MaxValue)]
        public required int BookId { get; set; }
        public Book? Book { get; set; }
        [Range(1, int.MaxValue)]
        public required int BasketId { get; set; }
    }
}
