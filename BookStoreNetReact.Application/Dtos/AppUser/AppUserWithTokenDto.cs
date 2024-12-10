namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class AppUserWithTokenDto
    {
        public AppUserDetailDto User {  get; set; } = new AppUserDetailDto();
        public TokenDto Token { get; set; } = new TokenDto();
    }
}
