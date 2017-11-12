using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiring.Models
{
    public class Employer
    {
        public int EmployerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ContactNumber { get; set; }
        public string Username { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationInfo { get; set; }
        public virtual ICollection<Job> Jobs {get; set; }
    }
}