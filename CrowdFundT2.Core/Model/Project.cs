using System;
using System.Collections.Generic;

namespace CrowdFundT2.Core.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photos { get; set; }
        public string Videos { get; set; }
        public string PostStatusUpdates { get; set; }
        public int Progress { get; set; }
        public DateTimeOffset Created { get; set; }
        public ProjectCategory Category { get; set; }
        public decimal ProjectCost { get; set; }
        public List<Package> Packages { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public decimal Earnings { get; set; }

        public Project()
        {
            Created = DateTimeOffset.Now;
            Packages = new List<Package>();
        }
    }
}
