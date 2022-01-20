using CoalShortagePortal.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoalShortagePortal.WebApp.Models
{
    public class GenCriticalCoalCreateVM
    {
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Serial Number")]
        public int SerialNum { get; set; }

        [Required]
        [Display(Name = "Station")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Owner { get; set; }

        [Required]
        [Display(Name = "Total Capacity")]
        public double Capacity { get; set; }

        [Required]
        public RegionName Region { get; set; }

        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}
