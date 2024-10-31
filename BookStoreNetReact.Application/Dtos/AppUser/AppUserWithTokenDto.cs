namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class AppUserWithTokenDto : AppUserDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}