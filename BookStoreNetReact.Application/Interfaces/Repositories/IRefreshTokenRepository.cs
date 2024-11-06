using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetValidToken(string token);
        Task<RefreshToken?> GetToken(string token);
        Task SaveRefreshToken(RefreshToken token);
        void RemoveRefreshToken(RefreshToken token);
        void RemoveExpiredRefreshToken();
    }
}
