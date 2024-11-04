using BookStoreNetReact.Application.Dtos.AppUser;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface IAppUserService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<AppUserWithTokenDto?> LoginAsync(LoginDto loginDto);
    }
}
