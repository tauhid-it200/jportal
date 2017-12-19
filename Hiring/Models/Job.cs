using System;
using System.Collections.Generic;

namespace Hiring.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public int Vacancy { get; set; }
        public string JobDescription { get; set; }
        public string RequiredSkills { get; set; }
        public string RequiredExperience { get; set; }
        public string EducationalQualifications { get; set; }
        public string JobLocation { get; set; }
        public string Salary { get; set; }
        public string SpecialInfo { get; set; }
        public DateTime DateOfPublication { get; set; }
        public DateTime DateLastModified { get; set; }
        public DateTime Deadline { get; set; }
        public int EmployerId { get; set; }
        public virtual Employer Employer { get; set; }
        public virtual ICollection<JobSeeker> Applicants { get; set; }
    }
}