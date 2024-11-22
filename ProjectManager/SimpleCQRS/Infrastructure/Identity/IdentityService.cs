using Application.Common.Interfaces;
using Domain.Entities.Features.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Shared.Common.Models;
namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly IAuthorizationService _authorizationService;
        public IdentityService(UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
        }
        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.FindByNameAsync(userId);
            return user?.UserName;
        }
        public async Task<AppActionResultData<string>> CreateUserAsync(string username, string password)
        {
            var result = new AppActionResultData<string>();
            var user = new ApplicationUser
            {
                UserName = username,
                Email = username
            };
            var newUser = await _userManager.CreateAsync(user, password);
            return result.BuildResult(user.Id.ToString());
        }
        public async Task<bool> IsRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && await _userManager.IsInRoleAsync(user, role);
        }
    }
    
}
