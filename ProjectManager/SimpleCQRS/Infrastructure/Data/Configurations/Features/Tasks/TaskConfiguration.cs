using Domain.Entities.Features.Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Infrastructure.Data.Configurations.Features.Tasks
{
    public class TaskConfiguration : IEntityTypeConfiguration<ApplicationTasks>
    {
        public void Configure(EntityTypeBuilder<ApplicationTasks> builder)
        {
           builder.HasKey(t => t.Id);   
           builder.Property(x=>x.TaskName).
                IsRequired()
                .HasMaxLength(500);
        }
    }
}
