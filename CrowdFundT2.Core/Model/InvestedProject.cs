using System;
using System.Collections.Generic;

namespace CrowdFundT2.Core.Model
{
    public class InvestedProject
    {
        public int InvestedProjectId { get; set; }
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public Decimal? InvestedAmount { get; set; }
        public List<InvestedPackage> InvestedPackages { get; set; }

        public InvestedProject()
        {
            InvestedPackages = new List<InvestedPackage>();
        }
    }
}
