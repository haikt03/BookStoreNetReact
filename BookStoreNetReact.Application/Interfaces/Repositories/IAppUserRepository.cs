using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces.Repositories
{
    public interface IAppUserRepository
    {
        IQueryable<AppUser>? GetAll(FilterAppUserDto filterAppUserDto);
        Task<AppUser?> GetByIdAsync(int id);
        Task<AppUser?> GetByUserNameAsync(string username);
        Task<bool> AddAsync(AppUser appUser);
        Task<bool> UpdateAsync(AppUser appUser);
        Task<bool> DeleteAsync(AppUser appUser);
        Task<bool> CheckPasswordAsync(AppUser appUser, string password);
    }
}
