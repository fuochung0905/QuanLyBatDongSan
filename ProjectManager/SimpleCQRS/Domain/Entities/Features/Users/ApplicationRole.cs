using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Features.Users
{
    public class ApplicationRole : IdentityRole, IAuditableEntity
    {
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ApplicationRole(string roleName)
        {
            Name = roleName;
            Created = DateTimeOffset.Now;
            LastModified = DateTimeOffset.Now;
            CreatedBy = roleName;
            LastModifiedBy = roleName;
        }
    }
}
