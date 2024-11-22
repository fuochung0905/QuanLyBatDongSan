using Domain.Entities.Features.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.Data
{
    public static class InitialiserExtensions
    {
       public static async Task InitialDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            await initialiser.InitialiserAsync();
            await initialiser.InitialisePersistedGrantDbAsync();
            await initialiser.SeedAsync();
        }
    }
    public class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly PersistedGrantDbContext _persistedGrantDbContext;
        public ApplicationDbContextInitialiser(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, PersistedGrantDbContext persistedGrantDbContext)
        {
            _context = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _persistedGrantDbContext = persistedGrantDbContext; 
        }
        public async Task InitialiserAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task InitialisePersistedGrantDbAsync()
        {
            try
            {
                if ((await _persistedGrantDbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _persistedGrantDbContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task TrySeedAsync()
        {
            var AdministratorRole = new ApplicationRole("Administrator");
            if (!_roleManager.Roles.All(u => u.Name != "Administrator"))
            {
                await _roleManager.CreateAsync(AdministratorRole);
            }
            var superAdmin = new ApplicationUser
            {
                UserName = "SUPERADMIN",
                Email = "phuochungnguyen.work@gmail.com"
            };
            if (!_userManager.Users.All(x => x.UserName == superAdmin.UserName))
            {
                await _userManager.CreateAsync(superAdmin, "admin123456");
                if (!string.IsNullOrWhiteSpace(AdministratorRole.Name))
                {
                    await _userManager.AddToRolesAsync(superAdmin, new[] { AdministratorRole.Name });
                }
            }
        }
        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}