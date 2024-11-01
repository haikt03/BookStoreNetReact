namespace BookStoreNetReact.Application.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public required int Id { get; set; }
        public string? Name { get; set; }
        public int? PId { get; set; }
    }
}
