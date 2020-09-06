using System;

namespace CrowdFundT2.Core.Services.Options
{
    public class SearchProjectOptions
    {
        public int? ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photos { get; set; }
        public string Videos { get; set; }
        public string PostStatusUpdates { get; set; }
        public ProjectCategory? Category { get; set; }
        public DateTimeOffset? CreatedFrom { get; set; }
        public DateTimeOffset? CreatedTo { get; set; }
        public Decimal? TotalAmountFrom { get; set; }
        public Decimal? TotalAmountTo { get; set; }
    }
}
