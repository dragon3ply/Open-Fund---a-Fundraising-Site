using CrowdFundT2.Core.Model;
using System;
using System.Collections.Generic;

namespace CrowdFundT2.Core.Services.Options
{
    public class SearchClientOptions
    {
        public int? ClientId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset? CreatedFrom { get; set; }
        public DateTimeOffset? CreatedTo { get; set; }
        public List<InvestedProject> InvestedProjects { get; set; }
        public List<Project> Project { get; set; }
    }
}
