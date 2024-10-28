namespace BookStoreNetReact.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
