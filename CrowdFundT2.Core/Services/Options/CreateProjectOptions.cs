using System;

namespace CrowdFundT2.Core.Services.Options
{
    public class CreateProjectOptions
    {
        public int? ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photos { get; set; }
        public string Videos { get; set; }
        public string PostStatusUpdates { get; set; }
        public int Progress { get; set; }
        public ProjectCategory? Category { get; set; }
        public Decimal? ProjectCost { get; set; }
    }
}
