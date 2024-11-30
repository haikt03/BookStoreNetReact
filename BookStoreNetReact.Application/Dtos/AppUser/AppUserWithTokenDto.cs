namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class AppUserWithTokenDto
    {
        public required AppUserDetailDto User {  get; set; }
        public required TokenDto Token { get; set; }
    }
}
