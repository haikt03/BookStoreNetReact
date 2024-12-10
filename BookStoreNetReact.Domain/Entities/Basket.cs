using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        [Range(1, int.MaxValue)]
        public required int UserId { get; set; }
        public AppUser? User { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
