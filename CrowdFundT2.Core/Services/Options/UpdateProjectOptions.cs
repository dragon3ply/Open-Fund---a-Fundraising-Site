using CrowdFundT2.Core.Model;
using System;
using System.Collections.Generic;

namespace CrowdFundT2.Core.Services.Options
{
    public class UpdateProjectOptions
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photos { get; set; }
        public string Videos { get; set; }
        public string PostStatusUpdates { get; set; }
        public int Progress { get; set; }
        public ProjectCategory? Category { get; set; }
        public Decimal? TotalAmount { get; set; }
        public List<Package> FundingPackages { get; set; }
    }
}
