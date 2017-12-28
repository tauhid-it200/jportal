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
            if (Session["jobSeekerId"] == null)
            {
                Session.Add("message", "Currently you are not logged in. Please login to apply!");

                return RedirectToAction("JobSeekerLogin", "Login");
            }

            var job = Repository.GetJobById(id);
            var employer = Repository.GetEmployerById(job.EmployerId);
            var jobSeekerId = (int)Session["jobSeekerId"];
            var jobSeeker = Repository.GetJobSeekerById(jobSeekerId);

            var viewModel = new JobApplicationViewModel()
            {
                Job = job,
                EmployerId = employer.EmployerId,
                OrganizationName = employer.OrganizationName,
                JobSeekerId = jobSeeker.JobSeekerId,
                JobSeekerUsername = jobSeeker.Username
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
                var jobSeekerId = jobApplication.JobSeekerId;
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

        public ActionResult JobBookmarking(int id)
        {
            if (Session["jobSeekerId"] == null)
            {
                Session.Add("message", "You have to login to use this feature!");

                return RedirectToAction("JobDetailsForJobSeeker", "JobSeeker", new {id});
            }
            var jobId = id;
            var jobSeekerId = (int)Session["jobSeekerId"];
            var bookmarkedJob = new BookmarkedJob()
            {
                JobId = jobId,
                JobSeekerId = jobSeekerId,
                DateOfBookmarking = DateTime.Now
            };
            var isBookmarkSuccessful = Repository.BookmarkJob(bookmarkedJob);

            if (!isBookmarkSuccessful)
            {
                Session.Add("message", "Failed to bookmark the job!");
            }

            Session.Add("message", "Your have successfully bookmarked the job!");

            return RedirectToAction("JobDetailsForJobSeeker", "JobSeeker", new { @id = jobId });
        }

        public ActionResult AllJobList()
        {
            var jobList = Repository.GetAllJobs();
            var appliedJobIdList = new List<int>();
            if (Session["jobSeekerId"] != null)
            {
                var appliedJobList = Repository.GetAppliedJobsByJobSeekerId((int)Session["jobSeekerId"]);
                foreach (var appliedJob in appliedJobList)
                {
                    appliedJobIdList.Add(appliedJob.JobId);
                }                
            }
            var searchCategories = new List<SelectListItem>
            {
                new SelectListItem {Text = "Job Title", Value = "0", Selected = true},
                new SelectListItem {Text = "Job Location", Value = "1"},
                new SelectListItem {Text = "Organization", Value = "2"},
                new SelectListItem {Text = "Skill", Value = "3"}
            };

            var viewModel = new AllJobListViewModel()
            {
                JobList = jobList,
                SearchCategories = searchCategories,
                AppliedJobIdList = appliedJobIdList
            };

            return View(viewModel);
        }

        public ActionResult JobSearching(AllJobListViewModel allJobList)
        {
            var category = allJobList.SearchCategory;
            var keyWord = allJobList.SearchKeyword;
            var searchedJobs = Repository.GetJobsBySearching(category, keyWord);
            var appliedJobIdList = new List<int>();
            if (Session["jobSeekerId"] != null)
            {
                var appliedJobList = Repository.GetAppliedJobsByJobSeekerId((int)Session["jobSeekerId"]);
                foreach (var appliedJob in appliedJobList)
                {
                    appliedJobIdList.Add(appliedJob.JobId);
                }
            }
            var searchCategories = new List<SelectListItem>
            {
                new SelectListItem {Text = "Job Title", Value = "0", Selected = true},
                new SelectListItem {Text = "Job Location", Value = "1"},
                new SelectListItem {Text = "Organization", Value = "2"},
                new SelectListItem {Text = "Skill", Value = "3"}
            };

            var viewModel = new AllJobListViewModel()
            {
                JobList = searchedJobs,
                SearchCategories = searchCategories,
                AppliedJobIdList = appliedJobIdList
            };

            return View(viewModel);
        }

        public ActionResult JobDetailsForJobSeeker(int id)
        {
            var job = Repository.GetJobById(id);
            var employer = Repository.GetEmployerById(job.EmployerId);
            var isAlreadyApplied = false;
            var isAlreadyBookmarked = false;

            if (Session["jobSeekerId"] != null)
            {
                var jobSeekerId = (int)Session["jobSeekerId"];
                isAlreadyApplied = Repository.CheckIfAlreadyApplied(job.JobId, jobSeekerId);
                isAlreadyBookmarked = Repository.CheckIfAlreadyBookmarked(job.JobId, jobSeekerId);
            }

            var viewModel = new JobDetailsViewModel()
            {
                Job = job,
                Employer = employer,
                IsAlreadyApplied = isAlreadyApplied,
                IsAlreadyBookmarked = isAlreadyBookmarked
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
            var bookmarkedJobs = Repository.GetBookmarkedJobsByJobSeekerId(id);

            return View(bookmarkedJobs);
        }

        public ActionResult JobSeekerProfile()
        {
            return View();
        }

        public ActionResult JobSeekerPasswordChange()
        {
            return View();
        }
    }
}