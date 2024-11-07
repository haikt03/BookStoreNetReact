using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetValidToken(string token);
        Task<RefreshToken?> GetToken(string token);
        Task SaveToken(RefreshToken token);
        void RemoveToken(RefreshToken token);
        void RemoveExpiredToken();
    }
}
