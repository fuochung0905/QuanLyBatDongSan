using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Features.Users
{
    public class ApplicationUser : IdentityUser, IAuditableEntity
    {
        public DateTime? DateOfBirth { get; set; }
        public string FullName { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
