using CoalShortagePortal.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoalShortagePortal.Core.Entities
{
    public class ExpectedRevivalResponse : AuditableEntity, IAggregateRoot
    {
        [Column(TypeName = "date")]
        public DateTime DataDate { get; set; }

        [Required]
        public int RTOutageId { get; set; }

        [Required]
        public string ElementOwners { get; set; }

        [Required]
        public string ElementName { get; set; }

        [Required]
        public double InstalledCapacity { get; set; }

        [Required]
        public string OutageReason { get; set; }

        [Required]
        public string OutageType { get; set; }

        [Required]
        public DateTime OutageDateTime { get; set; }

        public DateTime? ExpectedRevivalTime { get; set; }

        public string Remarks { get; set; }

    }
}
