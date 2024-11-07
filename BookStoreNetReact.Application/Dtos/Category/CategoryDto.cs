namespace BookStoreNetReact.Application.Dtos.Category
{
    public class CategoryDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public int? PId { get; set; }
    }
}
