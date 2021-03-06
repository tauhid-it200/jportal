﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Hiring.Models;

namespace Hiring.Repositories
{
    public class Repository
    {
        private enum SearchCategories { JobTitle, JobLocation, OrganizationName, Skill }
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

        public List<Job> GetJobsByEmployerId(int employerId)
        {
            return Db.Jobs.Where(m => m.EmployerId == employerId).Include(m => m.Applicants).ToList();
        }

        public List<Job> GetJobsBySearching(int category, string keyWord)
        {
            List<Job> searchedJobList;
            switch (category)
            {
                case (int)SearchCategories.JobTitle:
                    searchedJobList = Db.Jobs.Include(m => m.Employer).Where(m => m.JobTitle.Contains(keyWord)).ToList();
                    break;
                case (int)SearchCategories.JobLocation:
                    searchedJobList = Db.Jobs.Include(m => m.Employer).Where(m => m.JobLocation.Contains(keyWord)).ToList();
                    break;
                case (int)SearchCategories.OrganizationName:
                    searchedJobList = Db.Jobs.Include(m => m.Employer).Where(m => m.Employer.OrganizationName.Contains(keyWord)).ToList();
                    break;
                default:
                    searchedJobList = Db.Jobs.Include(m => m.Employer).Where(m => m.RequiredSkills.Contains(keyWord)).ToList();
                    break;
            }

            return searchedJobList;
        }

        //public List<JobSeeker> GetApplicantsByJobId(int jobId)
        //{
        //    return Db.Jobs.Where(m => m.JobId == jobId).Include(m => m.Applicants).FirstOrDefault();
        //}

        public Job GetJobById(int id)
        {
            return Db.Jobs.Where(m => m.JobId == id).Include(m => m.Applicants).FirstOrDefault();
        }

        public AppliedJob GetAppliedJobById(int id)
        {
            return Db.AppliedJobs.Where(m => m.AppliedJobId == id).Include(m => m.JobSeeker).FirstOrDefault();
        }

        public AppliedJob GetAppliedJobByJobId(int jobId)
        {
            return Db.AppliedJobs.Where(m => m.JobId == jobId).Include(m => m.JobSeeker).FirstOrDefault();
        }

        public List<AppliedJob> GetAppliedJobListByJobId(int jobId)
        {
            return Db.AppliedJobs.Where(m => m.JobId == jobId).Include(m => m.JobSeeker).ToList();
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

        public bool BookmarkJob(BookmarkedJob bookmarkedJob)
        {
            try
            {
                Db.BookmarkedJobs.Add(bookmarkedJob);

                Db.SaveChanges();

                return true;
            }
            catch (Exception)
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

        public bool CheckIfAlreadyBookmarked(int jobId, int jobSeekerId)
        {
            var bookmarkedJobCollection = Db.BookmarkedJobs.Where(m => m.JobId == jobId);

            foreach (var bookmarkedJob in bookmarkedJobCollection)
            {
                if (jobSeekerId == bookmarkedJob.JobSeekerId)
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

        public List<BookmarkedJob> GetBookmarkedJobsByJobSeekerId(int jobSeekerId)
        {
            return Db.BookmarkedJobs.Where(m => m.JobSeekerId == jobSeekerId).Include(m => m.Job).ToList();
        }
    }
}