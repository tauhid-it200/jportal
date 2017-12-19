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
        public Employer Employer { get; set; }
        public JobSeeker JobSeeker { get; set; }
        public int ExpectedSalary { get; set; }
    }
}