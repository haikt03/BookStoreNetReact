namespace BookStoreNetReact.Domain.Entities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }

        public int? PId { get; set; }
        public Category? PCategory { get; set; }

        public List<Category>? CCategories { get; set; }
        public List<Book>? Books { get; set; }
    }
}
