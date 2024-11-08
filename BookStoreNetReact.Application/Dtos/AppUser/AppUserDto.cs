namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class AppUserDto
    {
        public required int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string FullName { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
