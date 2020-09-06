using CrowdFundT2.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdFundT2.Web.Models
{
    public class CfModel
    {
        public List<Client> Client { get; set; }
        public List<Project> Projects { get; set; }

        public CfModel()
        {
            Client = new List<Client>();
            Projects = new List<Project>();
        }
    }
}
