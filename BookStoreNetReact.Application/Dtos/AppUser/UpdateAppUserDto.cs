using BookStoreNetReact.Application.Dtos.Image;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class UpdateAppUserDto
    {
        public required int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public UploadImageDto? Image { get; set; }
    }
}
