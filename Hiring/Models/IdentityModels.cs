using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hiring.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<BookmarkedJob> BookmarkedJobs { get; set; }
        public DbSet<AppliedJob> AppliedJobs { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}