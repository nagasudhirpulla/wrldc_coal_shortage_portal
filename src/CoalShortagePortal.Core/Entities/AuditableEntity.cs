using Microsoft.AspNetCore.Identity;
using System;

namespace CoalShortagePortal.Core.Entities
{
    public class AuditableEntity : BaseEntity
    {
        public IdentityUser CreatedBy { get; set; }
        public string CreatedById { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public IdentityUser LastModifiedBy { get; set; }
        public string LastModifiedById { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
