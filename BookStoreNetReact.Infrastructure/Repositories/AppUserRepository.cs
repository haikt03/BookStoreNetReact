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

        public IQueryable<AppUser> GetAllWithFilter(FilterAppUserDto filterDto)
        {
            var users = _userManager.Users
                .Search(filterDto.FullNameSearch, filterDto.EmailSearch, filterDto.PhoneNumberSearch)
                .Sort(filterDto.Sort);
            return users;
        }

        public async Task<AppUser?> GetByIdAsync(int userId, string? includeProperties = null)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = GetQueryWithIncludedProperties(query, includeProperties);
            }
            var result = await query.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<AppUser?> GetByUserNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user;
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
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

        public void UpdateUserAddress(UserAddress address)
        {
            _context.UserAddresses.Update(address);
        }

        public async Task<IdentityResult?> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return token;
        }

        public async Task<IdentityResult?> ConfirmEmailAsync(AppUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public static IQueryable<AppUser> GetQueryWithIncludedProperties(IQueryable<AppUser> query, string includeProperties)
        {
            var props = includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var prop in props)
            {
                query = query.Include(prop);
            }
            return query;
        }
    }
}
