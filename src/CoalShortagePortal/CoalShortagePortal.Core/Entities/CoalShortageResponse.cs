using CoalShortagePortal.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoalShortagePortal.Core.Entities
{
    public class CoalShortageResponse: BaseEntity, IAggregateRoot
    {
        [Column(TypeName = "date")]
        public DateTime DataDate { get; set; }
        [Required]
        public string Station { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Agency { get; set; }
        public double Capacity { get; set; }
        public double PrevDayAvgMw { get; set; }
        public double GenLossMw { get; set; }
        public string Remarks { get; set; }
    }
}
