namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobSeekerUpdateValidationMessages : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobSeekers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.JobSeekers", "Password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.JobSeekers", "Username", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobSeekers", "Username", c => c.String());
            AlterColumn("dbo.JobSeekers", "Password", c => c.String());
            AlterColumn("dbo.JobSeekers", "Name", c => c.String(nullable: false));
        }
    }
}
