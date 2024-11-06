namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class AppUserWithTokenDto : AppUserDto
    {
        public required TokenDto Token { get; set; }
    }
}