using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken?> GetValidToken(string token)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync
            (
                r => r.Token == token 
                && r.ExpiresAt >= DateTime.UtcNow
                && r.RevokedAt == null
            );
            return refreshToken;
        }
        public async Task<RefreshToken?> GetToken(string token)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
            return refreshToken;
        }

        public async Task SaveToken(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
        }

        public void RemoveToken(RefreshToken token)
        {
            _context.RefreshTokens.Remove(token);
        }

        public void RemoveExpiredToken()
        {
            var tokens = _context.RefreshTokens.Where(r => r.ExpiresAt < DateTime.UtcNow);
            _context.RefreshTokens.RemoveRange(tokens);
        }
    }
}
