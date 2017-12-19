using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Hiring.Models;

namespace Hiring.Repositories
{
    public class Repository
    {
        private ApplicationDbContext Db { get; set; }

        public Repository()
        {
            Db = new ApplicationDbContext();
        }

        public bool RegisterEmployer(Employer employer)
        {
            try
            {
                Db.Employers.Add(employer);
                Db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LoginEmployer(string username, string password)
        {
            var employer = GetEmployerByUsername(username);

            if (employer == null || employer.Password != password)
            {
                return false;
            }

            return true;
        }

        public bool RegisterJobSeeker(JobSeeker jobSeeker)
        {
            try
            {
                Db.JobSeekers.Add(jobSeeker);
                Db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LoginJobSeeker(string username, string password)
        {
            var jobSeeker = GetJobSeekerByUsername(username);

            if (jobSeeker == null || jobSeeker.Password != password)
            {
                return false;
            }

            return true;
        }

        public Employer GetEmployerByUsername(string username)
        {
            return Db.Employers.SingleOrDefault(e => e.Username == username);
        }

        public JobSeeker GetJobSeekerByUsername(string username)
        {
            return Db.JobSeekers.SingleOrDefault(j => j.Username == username);
        }

        public Employer GetEmployerById(int id)
        {
            return Db.Employers.SingleOrDefault(e => e.EmployerId == id);
        }

        public JobSeeker GetJobSeekerById(int id)
        {
            return Db.JobSeekers.SingleOrDefault(j => j.JobSeekerId == id);
        }
        public bool PostNewJob(Job job)
        {
            try
            {
                Db.Jobs.Add(job);
                Db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateJob(Job job)
        {
            try
            {
                var jobToUpdate = GetJobById(job.JobId);

                jobToUpdate.JobTitle = job.JobTitle;
                jobToUpdate.Vacancy = job.Vacancy;
                jobToUpdate.JobDescription = job.JobDescription;
                jobToUpdate.RequiredSkills = job.RequiredSkills;
                jobToUpdate.RequiredExperience = job.RequiredExperience;
                jobToUpdate.EducationalQualifications = job.EducationalQualifications;
                jobToUpdate.JobLocation = job.JobLocation;
                jobToUpdate.Salary = job.Salary;
                jobToUpdate.SpecialInfo = job.SpecialInfo;
                jobToUpdate.DateLastModified = job.DateLastModified;
                jobToUpdate.Deadline = job.Deadline;

                Db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Job> GetAllJobs()
        {
            return Db.Jobs.Include(m => m.Employer).ToList();
        }

        public List<Job> GetJobsByEmployerId(int id)
        {
            return Db.Jobs.Where(m => m.EmployerId == id).ToList();
        }

        public Job GetJobById(int id)
        {
            return Db.Jobs.Where(m => m.JobId == id).Include(m => m.Applicants).FirstOrDefault();
        }

        public bool ApplyForJob(AppliedJob appliedJob)
        {
            try
            {
                var applicant = GetJobSeekerById(appliedJob.JobSeekerId);

                GetJobById(appliedJob.JobId).Applicants.Add(applicant);
                Db.AppliedJobs.Add(appliedJob);

                Db.SaveChanges();

                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool CheckIfAlreadyApplied(int jobId, int jobSeekerId)
        {
            var appliedJobCollection = Db.AppliedJobs.Where(m => m.JobId == jobId);

            foreach (var appliedJob in appliedJobCollection)
            {
                if (jobSeekerId == appliedJob.JobSeekerId)
                {
                    return true;
                }
            }

            return false;
        }

        public AppliedJob GetAppliedJob(int jobId, int jobSeekerId)
        {
            var appliedJobCollection = Db.AppliedJobs.Where(m => m.JobId == jobId).Include(m => m.Job);
            foreach (var appliedJob in appliedJobCollection)
            {
                if (jobSeekerId == appliedJob.JobSeekerId)
                {
                    return appliedJob;
                }
            }

            return null;
        }

        public List<AppliedJob> GetAppliedJobsByJobSeekerId(int jobSeekerId)
        {
            return Db.AppliedJobs.Where(m => m.JobSeekerId == jobSeekerId).Include(m => m.Job).ToList();
        }
    }
}