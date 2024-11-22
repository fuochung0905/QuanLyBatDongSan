
using Application.Common.Interfaces;
using Domain.Entities.Features.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
        public DbSet<ApplicationRole> AspNetRoles => Set<ApplicationRole>();
        public DbSet<ApplicationUser> AspNetUsers => Set<ApplicationUser>();
        public DbSet<ApplicationUser> AspNetUserRole => Set<ApplicationUser>();
    }
    
    
}
