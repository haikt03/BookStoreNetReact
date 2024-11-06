namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class ConfirmEmailDto
    {
        public required int UserId { get; set; }
        public required string Token { get; set; }
    }
}
