using System;
using System.Collections.Generic;

namespace CoalShortagePortal.WebApp.Models
{
    public class GenDataDTO
    {
        public string GeneratorName { get; set; }
        public Dictionary<string, List<double>> Data { get; set; } = new Dictionary<string, List<double>>();
        public List<DateTime> Timestamps { get; set; } = new List<DateTime>();
    }
}
