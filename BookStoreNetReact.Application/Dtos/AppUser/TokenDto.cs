namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class TokenDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
