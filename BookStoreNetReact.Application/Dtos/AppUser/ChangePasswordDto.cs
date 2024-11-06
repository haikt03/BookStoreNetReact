namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class ChangePasswordDto
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
