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

        public int OutageTypeId { get; set; }
        public string OutageType { get; set; }

        public string Reason { get; set; }
        public DateTime OutageDateTime { get; set; }
        public DateTime? ExpectedRevivalDateTime { get; set; } = null;

        public string OutageRemarks { get; set; }

        public string OutageTag { get; set; }

        public string ElementOwners { get; set; }

    }
}
