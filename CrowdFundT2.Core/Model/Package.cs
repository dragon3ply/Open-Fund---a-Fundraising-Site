using System;

namespace CrowdFundT2.Core.Model
{
    public class Package
    {
        public int PackageId { get; set; }
        public string Description { get; set; }
        public decimal Reward { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset Created { get; set; }

        public Package()
        {
            Created = DateTimeOffset.Now;
            IsActive = true;
        }
    }
}
