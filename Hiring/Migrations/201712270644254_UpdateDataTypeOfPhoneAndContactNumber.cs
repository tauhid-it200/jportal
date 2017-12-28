namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataTypeOfPhoneAndContactNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobSeekers", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Employers", "ContactNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employers", "ContactNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.JobSeekers", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
