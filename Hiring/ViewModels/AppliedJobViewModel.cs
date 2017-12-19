using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hiring.Models;

namespace Hiring.ViewModels
{
    public class AppliedJobViewModel
    {
        public AppliedJob AppliedJob { get; set; }
        public Employer Employer { get; set; }
    }
}