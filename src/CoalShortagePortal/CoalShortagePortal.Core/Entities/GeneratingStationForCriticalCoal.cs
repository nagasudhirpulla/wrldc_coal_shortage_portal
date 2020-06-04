using CoalShortagePortal.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoalShortagePortal.Core.Entities
{
    public class GeneratingStationForCriticalCoal : BaseEntity, IAggregateRoot
    {
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [Required]
        public int SerialNum { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Owner { get; set; }
        public double Capacity { get; set; }
        [Required]
        public RegionName Region { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}
