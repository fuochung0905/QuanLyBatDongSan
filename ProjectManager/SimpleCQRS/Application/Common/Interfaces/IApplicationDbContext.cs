using Domain.Entities.Features.Task;
using Domain.Entities.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<ApplicationRole> AspNetRoles { get; }
        public DbSet<ApplicationUser> AspNetUsers { get; }
        public DbSet<ApplicationUser> AspNetUserRole { get; }
        public DbSet<ApplicationTasks> Tasks { get; }
    }
}
