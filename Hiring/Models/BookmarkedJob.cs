using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiring.Models
{
    public class BookmarkedJob
    {
        public int BookmarkedJobId { get; set; }
        public int JobId { get; set; }
        public virtual Job Job { get; set; }
        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
    }
}