using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hiring.Models;

namespace Hiring.ViewModels
{
    public class ApplicantViewModel
    {
        public JobSeeker Applicant { get; set; }
        public int ExpectedSalary { get; set; }
        public DateTime AppliedOn { get; set; }
    }
}