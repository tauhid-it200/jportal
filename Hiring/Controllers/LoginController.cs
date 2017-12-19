using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hiring.Models;
using Hiring.Repositories;

namespace Hiring.Controllers
{
    public class LoginController : Controller
    {
        public Repository Repository { get; set; }

        public LoginController()
        {
            Repository = new Repository();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult JobSeekerLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JobSeekerLogin(JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
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

        public ActionResult EmployerLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployerLogin( Employer employer)
        {
            if (ModelState.IsValid)
            {
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

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Add("message", "You have successfully logged out!");

            return RedirectToAction("Login", "Login");
        }
	}
}