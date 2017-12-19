using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hiring.Models;
using Hiring.Repositories;

namespace Hiring.Controllers
{
    public class RegisterController : Controller
    {
        public Repository Repository { get; set; }

        public RegisterController()
        {
            Repository = new Repository();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JobSeekerRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobSeekerRegister(JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
                var isRegistrationSuccessful = Repository.RegisterJobSeeker(jobSeeker);

                if (!isRegistrationSuccessful)
                {
                    Session.Add("message", "JobSeeker registration failed!");

                    return RedirectToAction("JobSeekerRegister", "Register");
                }

                var isLoginSuccessful = Repository.LoginJobSeeker(jobSeeker.Username, jobSeeker.Password);

                if (!isLoginSuccessful)
                {
                    Session.Add("message", "Invalid username or password!");

                    return RedirectToAction("JobSeekerLogin", "Login");
                }

                var jobSeekerId = Repository.GetJobSeekerByUsername(jobSeeker.Username).JobSeekerId;

                Session.Add("jobSeekerId", jobSeekerId);

                return RedirectToAction("Home", "JobSeeker", new { @id = jobSeekerId });
            }

            return View(jobSeeker);
        }

        public ActionResult EmployerRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployerRegister(Employer employer)
        {
            if (ModelState.IsValid)
            {
                var isRegistrationSuccessful = Repository.RegisterEmployer(employer);

                if (!isRegistrationSuccessful)
                {
                    Session.Add("message", "Employer registration failed!");

                    return RedirectToAction("EmployerRegister", "Register");
                }

                var isLoginSuccessful = Repository.LoginEmployer(employer.Username, employer.Password);

                if (!isLoginSuccessful)
                {
                    Session.Add("message", "Invalid username or password!");

                    return RedirectToAction("EmployerLogin", "Login");
                }

                var employerId = Repository.GetEmployerByUsername(employer.Username).EmployerId;

                Session.Add("employerId", employerId);

                return RedirectToAction("Home", "Employer", new { @id = employerId });
            }

            return View(employer);
        }
    }
}