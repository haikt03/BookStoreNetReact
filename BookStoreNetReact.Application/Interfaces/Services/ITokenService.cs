using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(AppUser user);
        Task<string> GenerateRefreshToken(AppUser user);
        Task<bool> ValidateRefreshToken(AppUser user, string refreshToken);
    }
}
