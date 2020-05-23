using CoalShortagePortal.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoalShortagePortal.Core.Entities
{
    public class CriticalCoalResponse : AuditableEntity, IAggregateRoot
    {
        [Column(TypeName = "date")]
        public DateTime DataDate { get; set; }
        [NotMapped]
        public int SerialNum { get; set; }
        [Required]
        public string Station { get; set; }
        [Required]
        public string Owner { get; set; }
        public double Capacity { get; set; }
        public double PresentGenMw { get; set; }
        public double CoalGenLossMw { get; set; }
        public double PresentCoalStockDays { get; set; }
        public string Remarks { get; set; }
    }
}
