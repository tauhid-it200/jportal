namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobSeekerValidationAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobSeekers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.JobSeekers", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobSeekers", "Email", c => c.String());
            AlterColumn("dbo.JobSeekers", "Name", c => c.String());
        }
    }
}
