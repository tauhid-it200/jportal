namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployerUpdateValidationMessageForOrganization : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employers", "OrganizationName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employers", "OrganizationName", c => c.String());
        }
    }
}
