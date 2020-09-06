using System;
using System.Collections.Generic;
using System.Linq;

namespace CrowdFundT2.Core.Model
{
   public class Client
    {
        public int ClientId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset Created { get; set; }
        public List<InvestedProject> InvestedProjects { get; set; }
        public List<Project> Projects { get; set; }

        public Client()
        {
            Created = DateTimeOffset.Now;
            InvestedProjects = new List<InvestedProject>();
            Projects = new List<Project>();
        }
        
        //Check Email Validation
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
