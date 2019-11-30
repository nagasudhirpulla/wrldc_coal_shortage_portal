using System;
using System.ComponentModel.DataAnnotations;

namespace CoalShortagePortal.WebApp.Models
{
    public class GenCoalShortageCreateVM
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
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Agency { get; set; }

        [Required]
        [Display(Name = "Total Capacity")]
        public double Capacity { get; set; }

        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}
