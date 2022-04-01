using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoalShortagePortal.Core.ReportingData
{
    public class UnRevGeneratorOutage
    {
        public int RTOutageId { get; set; }

        public int ElementId { get; set; }
        public string ElementName { get; set; }

        public double InstalledCapacity { get; set; }

        public int OutageTypeId { get; set; }
        public string OutageType { get; set; }

        public string OutageReason { get; set; }
        public DateTime OutageDateTime { get; set; }
        public DateTime? ExpectedDateTime { get; set; }

        public string OutageTag { get; set; }

        public string ElementOwners { get; set; }

    }
}
