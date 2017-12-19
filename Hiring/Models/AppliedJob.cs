using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiring.Models
{
    public class AppliedJob
    {
        public int AppliedJobId { get; set; }
        public int JobId { get; set; }
        public virtual Job Job { get; set; }
        public int JobSeekerId { get; set; }
        public virtual JobSeeker JobSeeker { get; set; }
        public int ExpectedSalary { get; set; }
        public DateTime DateOfApplication { get; set; }
    }
}