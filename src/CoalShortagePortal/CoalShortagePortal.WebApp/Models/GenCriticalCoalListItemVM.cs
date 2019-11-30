using System;
using System.ComponentModel.DataAnnotations;

namespace CoalShortagePortal.WebApp.Models
{
    public class GenCriticalCoalListItemVM
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Serial Number")]
        public int SerialNum { get; set; }
        
        [Display(Name = "Station")]
        public string Name { get; set; }
        public string Owner { get; set; }

        [Display(Name = "Total Capacity (MW)")]
        public double Capacity { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
