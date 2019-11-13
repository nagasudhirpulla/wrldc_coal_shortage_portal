using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoalShortagePortal.Core.Entities
{
    public class GeneratingStationForCoalShortage
    {
        public int GeneratingStationForCoalShortageId { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public string Location { get; set; }
        public string Agency { get; set; }
        public double Capacity { get; set; }
    }
}
