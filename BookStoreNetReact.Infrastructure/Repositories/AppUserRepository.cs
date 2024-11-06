using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public AppUserRepository(UserManager<AppUser> userManager, AppDbContext context)
        { 
            _userManager = userManager;
            _context = context;
        }

        public IQueryable<AppUser>? GetAll(FilterAppUserDto filterDto)
        {
            var users = _userManager.Users
                .Search(filterDto.Search)
                .Sort(filterDto.Sort);
            return users;
        }

        public async Task<AppUser?> GetByIdAsync(int userId)
        {
            var user = await _userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<AppUser?> GetByUserNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user;
        }

        public async Task<IdentityResult?> AddAsync(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "Member");
            return result;
        }

        public async Task<IdentityResult?> UpdateAsync(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult?> RemoveAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<UserAddress?> GetUserAddressByIdAsync(int addressId)
        {
            var address = await _context.UserAddresses.FindAsync(addressId);
            return address;
        }

        public void UpdateUserAddress(UserAddress address)
        {
            _context.UserAddresses.Update(address);
        }
    }
}
