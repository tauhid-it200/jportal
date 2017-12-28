using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hiring.Models;

namespace Hiring.ViewModels
{
    public class JobApplicationViewModel
    {
        public Job Job { get; set; }
        public int EmployerId { get; set; }
        public string OrganizationName { get; set; }
        public int JobSeekerId { get; set; }
        public string JobSeekerUsername { get; set; }
        public int ExpectedSalary { get; set; }
    }
}