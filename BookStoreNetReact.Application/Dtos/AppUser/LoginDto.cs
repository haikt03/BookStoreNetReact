namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class LoginDto : AppUserDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}