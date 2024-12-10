namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string PublicId { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
