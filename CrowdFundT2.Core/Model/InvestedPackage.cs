    using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundT2.Core.Model
{
    public class InvestedPackage
    {
        public int PackageId { get; set; }
        public int InvestedProjectId { get; set; }
        public InvestedProject InvestedProject { get; set; }
        public Package Package { get; set; }
    }
}
