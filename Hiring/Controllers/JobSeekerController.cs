using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hiring.Models;
using Hiring.Repositories;
using Hiring.ViewModels;

namespace Hiring.Controllers
{
    public class JobSeekerController : Controller
    {
        public Repository Repository { get; set; }

        public JobSeekerController()
        {
            Repository = new Repository();
        }
        public ActionResult Home(int id)
        {
            return View();
        }
        public ActionResult JobApplication(int id)
        {
            var job = Repository.GetJobById(id);
            var employer = Repository.GetEmployerById(job.EmployerId);
            var jobSeekerId = (int)Session["jobSeekerId"];
            var jobSeeker = Repository.GetJobSeekerById(jobSeekerId);

            var viewModel = new JobApplicationViewModel()
            {
                Job = job,
                Employer = employer,
                JobSeeker = jobSeeker
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobApplication(JobApplicationViewModel jobApplication)
        {
            if (ModelState.IsValid)
            {
                var jobId = jobApplication.Job.JobId;
                var jobSeekerId = jobApplication.JobSeeker.JobSeekerId;
                var expectedsalary = jobApplication.ExpectedSalary;
                var appliedJob = new AppliedJob()
                {
                    JobId = jobId,
                    JobSeekerId = jobSeekerId,
                    ExpectedSalary = expectedsalary,
                    DateOfApplication = DateTime.Now
                };
                var isApplicationSuccessful = Repository.ApplyForJob(appliedJob);

                if (!isApplicationSuccessful)
                {
                    Session.Add("message", "Your application for the job could not be completed!");

                    return RedirectToAction("JobApplication", "JobSeeker", new { @id = jobId });
                }

                Session.Add("message", "Your have successfully applied for the following post:");

                return RedirectToAction("ConfirmJobApplication", "JobSeeker", new { @id = jobId });
            }

            return View(jobApplication);
        }

        public ActionResult ConfirmJobApplication(int id)
        {
            var jobId = id;
            var jobSeekerId = (int)Session["jobSeekerId"];
            var jobSeeker = Repository.GetJobSeekerById(jobSeekerId);
            var appliedJob = Repository.GetAppliedJob(jobId, jobSeekerId);
            var employer = Repository.GetEmployerById(appliedJob.Job.EmployerId); 

            var viewModel = new ConfirmJobApplicationViewModel()
            {
                AppliedJob = appliedJob,
                JobSeeker = jobSeeker,
                Employer = employer
            };

            return View(viewModel);
        }

        public ActionResult AllJobList()
        {
            var jobs = Repository.GetAllJobs();

            return View(jobs);
        }

        public ActionResult JobDetailsForJobSeeker(int id)
        {
            var job = Repository.GetJobById(id);
            var employer = Repository.GetEmployerById(job.EmployerId);
            var jobSeekerId = (int)Session["jobSeekerId"];
            var isAlreadyApplied = Repository.CheckIfAlreadyApplied(job.JobId, jobSeekerId);
            
            var viewModel = new JobDetailsViewModel()
            {
                Job = job,
                Employer = employer,
                IsAlreadyApplied = isAlreadyApplied
            };

            return View(viewModel);
        }

        public ActionResult AppliedJobList(int id)
        {
            var appliedJobs = Repository.GetAppliedJobsByJobSeekerId(id);

            return View(appliedJobs);
        }

        public ActionResult BookmarkedJobList(int id)
        {
            return View();
        }
    }
}