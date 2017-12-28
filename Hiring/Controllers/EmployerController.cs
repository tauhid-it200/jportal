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
    public class EmployerController : Controller
    {
        public Repository Repository { get; set; }

        public EmployerController()
        {
            Repository = new Repository();
        }
        public ActionResult Home(int id)
        {
            if (Session["employerId"] == null)
            {
                Session.Add("message", "You are not logged in!");

                return RedirectToAction("EmployerLogin", "Login");
            }

            return View();
        }
        public ActionResult JobPosting()
        {
            if (Session["employerId"] == null)
            {
                Session.Add("message", "You are not logged in!");

                return RedirectToAction("EmployerLogin", "Login");
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobPosting(Job job)
        {
            if (ModelState.IsValid)
            {
                var isPosted = Repository.PostNewJob(job);

                if (!isPosted)
                {
                    Session.Add("message", "Failed to post the job!");

                    return View(job);
                }

                Session.Add("message", "The job has been posted successfully!");
            }

            return RedirectToAction("EmployerJobList", "Employer", new { @id = job.EmployerId });
        }
        public ActionResult JobEditing(int id)
        {
            var job = Repository.GetJobById(id);

            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobEditing(Job job)
        {
            if (ModelState.IsValid)
            {
                var isPosted = Repository.UpdateJob(job);

                if (!isPosted)
                {
                    Session.Add("message", "Failed to update the job!");

                    return View(job);
                }

                Session.Add("message", "The job has been updated successfully!");

                return RedirectToAction("JobDetailsForEmployer", "Employer", new { @id = job.JobId });
            }

            return View(job);
        }
        public ActionResult EmployerJobList(int id)
        {
            //if (id != (int)Session["employerId"])
            //{
            //    Session.Add("message", "You are not logged in!");

            //    return RedirectToAction("EmployerLogin", "Login");
            //}

            var postedJobs = Repository.GetJobsByEmployerId(id);

            return View(postedJobs);
        }
        public ActionResult JobDetailsForEmployer(int id)
        {
            var job = Repository.GetJobById(id);
            return View(job);
        }
        public ActionResult ApplicantList(int id)
        {
            var appliedJobs = Repository.GetAppliedJobListByJobId(id);

            return View(appliedJobs);
        }
        public ActionResult ApplicantDetails(int id)
        {
            var appliedJob = Repository.GetAppliedJobById(id);

            return View(appliedJob);
        }

        public ActionResult EmployerPasswordChange()
        {
            return View();
        }

        public ActionResult EmployerProfile()
        {
            return View();
        }
	}
}