namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesReformed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobSeekerAppliedJobs", "JobSeeker_JobSeekerId", "dbo.JobSeekers");
            DropForeignKey("dbo.JobSeekerAppliedJobs", "AppliedJob_AppliedJobId", "dbo.AppliedJobs");
            DropForeignKey("dbo.BookmarkedJobJobSeekers", "BookmarkedJob_BookmarkedJobId", "dbo.BookmarkedJobs");
            DropForeignKey("dbo.BookmarkedJobJobSeekers", "JobSeeker_JobSeekerId", "dbo.JobSeekers");
            DropIndex("dbo.JobSeekerAppliedJobs", new[] { "JobSeeker_JobSeekerId" });
            DropIndex("dbo.JobSeekerAppliedJobs", new[] { "AppliedJob_AppliedJobId" });
            DropIndex("dbo.BookmarkedJobJobSeekers", new[] { "BookmarkedJob_BookmarkedJobId" });
            DropIndex("dbo.BookmarkedJobJobSeekers", new[] { "JobSeeker_JobSeekerId" });
            AddColumn("dbo.AppliedJobs", "JobSeekerId", c => c.Int(nullable: false));
            AddColumn("dbo.AppliedJobs", "DateOfApplication", c => c.DateTime(nullable: false));
            AddColumn("dbo.JobSeekers", "Job_JobId", c => c.Int());
            AddColumn("dbo.BookmarkedJobs", "JobSeekerId", c => c.Int(nullable: false));
            AddColumn("dbo.BookmarkedJobs", "DateOfBookmarking", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "DateOfPublication", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Jobs", "Deadline", c => c.DateTime(nullable: false));
            CreateIndex("dbo.AppliedJobs", "JobSeekerId");
            CreateIndex("dbo.BookmarkedJobs", "JobSeekerId");
            CreateIndex("dbo.JobSeekers", "Job_JobId");
            AddForeignKey("dbo.AppliedJobs", "JobSeekerId", "dbo.JobSeekers", "JobSeekerId", cascadeDelete: true);
            AddForeignKey("dbo.BookmarkedJobs", "JobSeekerId", "dbo.JobSeekers", "JobSeekerId", cascadeDelete: true);
            AddForeignKey("dbo.JobSeekers", "Job_JobId", "dbo.Jobs", "JobId");
            DropColumn("dbo.Jobs", "PublicationDate");
            DropTable("dbo.JobSeekerAppliedJobs");
            DropTable("dbo.BookmarkedJobJobSeekers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookmarkedJobJobSeekers",
                c => new
                    {
                        BookmarkedJob_BookmarkedJobId = c.Int(nullable: false),
                        JobSeeker_JobSeekerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookmarkedJob_BookmarkedJobId, t.JobSeeker_JobSeekerId });
            
            CreateTable(
                "dbo.JobSeekerAppliedJobs",
                c => new
                    {
                        JobSeeker_JobSeekerId = c.Int(nullable: false),
                        AppliedJob_AppliedJobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobSeeker_JobSeekerId, t.AppliedJob_AppliedJobId });
            
            AddColumn("dbo.Jobs", "PublicationDate", c => c.String());
            DropForeignKey("dbo.JobSeekers", "Job_JobId", "dbo.Jobs");
            DropForeignKey("dbo.BookmarkedJobs", "JobSeekerId", "dbo.JobSeekers");
            DropForeignKey("dbo.AppliedJobs", "JobSeekerId", "dbo.JobSeekers");
            DropIndex("dbo.JobSeekers", new[] { "Job_JobId" });
            DropIndex("dbo.BookmarkedJobs", new[] { "JobSeekerId" });
            DropIndex("dbo.AppliedJobs", new[] { "JobSeekerId" });
            AlterColumn("dbo.Jobs", "Deadline", c => c.String());
            DropColumn("dbo.Jobs", "DateOfPublication");
            DropColumn("dbo.BookmarkedJobs", "DateOfBookmarking");
            DropColumn("dbo.BookmarkedJobs", "JobSeekerId");
            DropColumn("dbo.JobSeekers", "Job_JobId");
            DropColumn("dbo.AppliedJobs", "DateOfApplication");
            DropColumn("dbo.AppliedJobs", "JobSeekerId");
            CreateIndex("dbo.BookmarkedJobJobSeekers", "JobSeeker_JobSeekerId");
            CreateIndex("dbo.BookmarkedJobJobSeekers", "BookmarkedJob_BookmarkedJobId");
            CreateIndex("dbo.JobSeekerAppliedJobs", "AppliedJob_AppliedJobId");
            CreateIndex("dbo.JobSeekerAppliedJobs", "JobSeeker_JobSeekerId");
            AddForeignKey("dbo.BookmarkedJobJobSeekers", "JobSeeker_JobSeekerId", "dbo.JobSeekers", "JobSeekerId", cascadeDelete: true);
            AddForeignKey("dbo.BookmarkedJobJobSeekers", "BookmarkedJob_BookmarkedJobId", "dbo.BookmarkedJobs", "BookmarkedJobId", cascadeDelete: true);
            AddForeignKey("dbo.JobSeekerAppliedJobs", "AppliedJob_AppliedJobId", "dbo.AppliedJobs", "AppliedJobId", cascadeDelete: true);
            AddForeignKey("dbo.JobSeekerAppliedJobs", "JobSeeker_JobSeekerId", "dbo.JobSeekers", "JobSeekerId", cascadeDelete: true);
        }
    }
}
