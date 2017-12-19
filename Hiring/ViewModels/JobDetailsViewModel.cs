using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hiring.Models;

namespace Hiring.ViewModels
{
    public class JobDetailsViewModel
    {
        public Job Job { get; set; }
        public Employer Employer { get; set; }
        public bool IsAlreadyApplied { get; set; }
    }
}