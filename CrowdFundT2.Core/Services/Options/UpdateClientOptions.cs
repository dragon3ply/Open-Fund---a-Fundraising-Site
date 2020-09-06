using CrowdFundT2.Core.Model;
using System.Collections.Generic;

namespace CrowdFundT2.Core.Services.Options
{
    public class UpdateClientOptions
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }
        public List<InvestedProject> InvestedProjects { get; set; }
        public List<Project> Project { get; set; }
    }
}