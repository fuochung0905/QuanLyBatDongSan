
using Application.Common.Interfaces;
using Domain.Entities.Features.Task;
using Domain.Entities.Features.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
        public DbSet<ApplicationTasks> Tasks => Set<ApplicationTasks>();

 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }


}
