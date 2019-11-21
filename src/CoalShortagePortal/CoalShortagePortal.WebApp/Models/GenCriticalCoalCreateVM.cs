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
        [Display(Name = "Station")]
        public string Name { get; set; }
        [Required]
        public string Owner { get; set; }

        [Required]
        [Display(Name = "Total Capacity")]
        public double Capacity { get; set; }

        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}
