using CoalShortagePortal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace CoalShortagePortal.Core.Entities
{
    public class GeneratingStationForCoalShortage : BaseEntity, IAggregateRoot
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
        public string Location { get; set; }
        [Required]
        public string Agency { get; set; }
        public double Capacity { get; set; }
        [Required]
        public RegionName Region { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}
