using System;

namespace CrowdFundT2.Core.Services.Options
{
    public class SearchPackageOptions
    {
        public int? PackageId { get; set; }
        public string Description { get; set; }
        public decimal? Reward { get; set; }
        public bool? IsActive { get; set; }
        public DateTimeOffset? CreatedFrom { get; set; }
        public DateTimeOffset? CreatedTo { get; set; }
    }
}
