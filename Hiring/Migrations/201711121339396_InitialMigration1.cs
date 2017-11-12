namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppliedJobs",
                c => new
                    {
                        AppliedJobId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppliedJobId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.JobSeekers",
                c => new
                    {
                        JobSeekerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Username = c.String(),
                        Skills = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.JobSeekerId);
            
            CreateTable(
                "dbo.BookmarkedJobs",
                c => new
                    {
                        BookmarkedJobId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookmarkedJobId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        Vacancy = c.Int(nullable: false),
                        JobDescription = c.String(),
                        RequiredSkills = c.String(),
                        RequiredExperience = c.String(),
                        EducationalQualifications = c.String(),
                        JobLocation = c.String(),
                        Salary = c.String(),
                        SpecialInfo = c.String(),
                        EmployerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Employers", t => t.EmployerId, cascadeDelete: true)
                .Index(t => t.EmployerId);
            
            CreateTable(
                "dbo.Employers",
                c => new
                    {
                        EmployerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ContactNumber = c.Int(nullable: false),
                        Username = c.String(),
                        OrganizationName = c.String(),
                        OrganizationInfo = c.String(),
                    })
                .PrimaryKey(t => t.EmployerId);
            
            CreateTable(
                "dbo.JobSeekerAppliedJobs",
                c => new
                    {
                        JobSeeker_JobSeekerId = c.Int(nullable: false),
                        AppliedJob_AppliedJobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobSeeker_JobSeekerId, t.AppliedJob_AppliedJobId })
                .ForeignKey("dbo.JobSeekers", t => t.JobSeeker_JobSeekerId, cascadeDelete: true)
                .ForeignKey("dbo.AppliedJobs", t => t.AppliedJob_AppliedJobId, cascadeDelete: true)
                .Index(t => t.JobSeeker_JobSeekerId)
                .Index(t => t.AppliedJob_AppliedJobId);
            
            CreateTable(
                "dbo.BookmarkedJobJobSeekers",
                c => new
                    {
                        BookmarkedJob_BookmarkedJobId = c.Int(nullable: false),
                        JobSeeker_JobSeekerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookmarkedJob_BookmarkedJobId, t.JobSeeker_JobSeekerId })
                .ForeignKey("dbo.BookmarkedJobs", t => t.BookmarkedJob_BookmarkedJobId, cascadeDelete: true)
                .ForeignKey("dbo.JobSeekers", t => t.JobSeeker_JobSeekerId, cascadeDelete: true)
                .Index(t => t.BookmarkedJob_BookmarkedJobId)
                .Index(t => t.JobSeeker_JobSeekerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppliedJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.BookmarkedJobJobSeekers", "JobSeeker_JobSeekerId", "dbo.JobSeekers");
            DropForeignKey("dbo.BookmarkedJobJobSeekers", "BookmarkedJob_BookmarkedJobId", "dbo.BookmarkedJobs");
            DropForeignKey("dbo.BookmarkedJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "EmployerId", "dbo.Employers");
            DropForeignKey("dbo.JobSeekerAppliedJobs", "AppliedJob_AppliedJobId", "dbo.AppliedJobs");
            DropForeignKey("dbo.JobSeekerAppliedJobs", "JobSeeker_JobSeekerId", "dbo.JobSeekers");
            DropIndex("dbo.AppliedJobs", new[] { "JobId" });
            DropIndex("dbo.BookmarkedJobJobSeekers", new[] { "JobSeeker_JobSeekerId" });
            DropIndex("dbo.BookmarkedJobJobSeekers", new[] { "BookmarkedJob_BookmarkedJobId" });
            DropIndex("dbo.BookmarkedJobs", new[] { "JobId" });
            DropIndex("dbo.Jobs", new[] { "EmployerId" });
            DropIndex("dbo.JobSeekerAppliedJobs", new[] { "AppliedJob_AppliedJobId" });
            DropIndex("dbo.JobSeekerAppliedJobs", new[] { "JobSeeker_JobSeekerId" });
            DropTable("dbo.BookmarkedJobJobSeekers");
            DropTable("dbo.JobSeekerAppliedJobs");
            DropTable("dbo.Employers");
            DropTable("dbo.Jobs");
            DropTable("dbo.BookmarkedJobs");
            DropTable("dbo.JobSeekers");
            DropTable("dbo.AppliedJobs");
        }
    }
}
