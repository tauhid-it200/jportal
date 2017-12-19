namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewPropertiesInJobAndApppliedJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppliedJobs", "ExpectedSalary", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "DateLastModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "DateLastModified");
            DropColumn("dbo.AppliedJobs", "ExpectedSalary");
        }
    }
}
