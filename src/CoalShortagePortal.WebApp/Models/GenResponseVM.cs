using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoalShortagePortal.Core.Entities;

namespace CoalShortagePortal.WebApp.Models
{
    public class GenResponseVM
    {
        [DataType(DataType.Date)]
        public DateTime RecordDate { get; set; }
        public List<CoalShortageResponse> CoalShortageResponses { get; set; } = new List<CoalShortageResponse>();
        public List<OtherReasonsResponse> OtherReasonsResponses { get; set; } = new List<OtherReasonsResponse>();
        public List<CriticalCoalResponse> CriticalCoalResponses { get; set; } = new List<CriticalCoalResponse>();
    }
}
