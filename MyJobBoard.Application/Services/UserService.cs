using Microsoft.AspNetCore.Identity;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;

namespace MyJobBoard.Application.Services
{
    public class UserService : IUserService
    {
         private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }
        public async Task<ApplicationUser?> GetUserById(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }
        public async Task<ApplicationUser?> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

    }
}
