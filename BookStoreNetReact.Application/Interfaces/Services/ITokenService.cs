using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(AppUser appUser);
        Task<string> GenerateRefreshToken(AppUser appUser);
        Task<bool> ValidateRefreshToken(AppUser appUser, string refreshToken);
    }
}
