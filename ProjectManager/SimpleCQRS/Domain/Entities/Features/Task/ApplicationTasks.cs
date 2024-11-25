using Domain.Common;

namespace Domain.Entities.Features.Task
{
    public class ApplicationTasks : BaseAuditableEntity<Guid>
    {
        public string TaskName { get; set; }

    }
}
