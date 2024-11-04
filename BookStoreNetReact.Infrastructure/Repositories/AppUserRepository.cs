using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public AppUserRepository(UserManager<AppUser> userManager)
        { 
            _userManager = userManager;
        }

        public IQueryable<AppUser>? GetAll(FilterAppUserDto filterAppUserDto)
        {
            var appUsers = _userManager.Users
                .Search(filterAppUserDto.Search)
                .Sort(filterAppUserDto.Sort);
            return appUsers;
        }

        public async Task<AppUser?> GetByIdAsync(int id)
        {
            var appUser = await _userManager.FindByIdAsync(id.ToString());
            return appUser;
        }

        public async Task<AppUser?> GetByUserNameAsync(string username)
        {
            var appUser = await _userManager.FindByNameAsync(username);
            return appUser;
        }

        public async Task<bool> AddAsync(AppUser appUser)
        {
            var result = await _userManager.CreateAsync(appUser);
            return result.Succeeded;
        }

        public async Task<bool> UpdateAsync(AppUser appUser)
        {
            var result = await _userManager.UpdateAsync(appUser);
            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(AppUser appUser)
        {
            var result = await _userManager.DeleteAsync(appUser);
            return result.Succeeded;
        }

        public async Task<bool> CheckPasswordAsync(AppUser appUser, string password)
        {
            var result = await _userManager.CheckPasswordAsync(appUser, password);
            return result;
        }
    }
}
