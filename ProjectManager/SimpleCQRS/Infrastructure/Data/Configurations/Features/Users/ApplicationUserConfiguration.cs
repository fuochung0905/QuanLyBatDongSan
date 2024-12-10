using Domain.Entities.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations.Features.Users
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(e => e.UserRoles)
           .WithOne(e => e.User)
           .HasForeignKey(ur => ur.UserId)
           .IsRequired();

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(550);
        }
    }
}
