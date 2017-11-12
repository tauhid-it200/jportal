using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiring.Models
{
    public class JobSeeker
    {
        public int JobSeekerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Skills { get; set; }
        public string Address { get; set; }
        public virtual ICollection<BookmarkedJob> BookmarkedJobs { get; set; }
        public virtual ICollection<AppliedJob> AppliedJobs { get; set; }
    }
}