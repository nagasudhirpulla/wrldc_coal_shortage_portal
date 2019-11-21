using System;
using System.ComponentModel.DataAnnotations;

namespace CoalShortagePortal.WebApp.Models
{
    public class GenCoalShortageListItemVM
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Station")]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Agency { get; set; }

        [Display(Name = "Total Capacity")]
        public double Capacity { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
