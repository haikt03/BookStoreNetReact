using Microsoft.AspNetCore.Http;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class UpdateAppUserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public IFormFile? File { get; set; }
    }
}
