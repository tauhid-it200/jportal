namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployerUpdateValidationMessages : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Employers", "Password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Employers", "Username", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employers", "Username", c => c.String());
            AlterColumn("dbo.Employers", "Password", c => c.String());
            AlterColumn("dbo.Employers", "Email", c => c.String());
            AlterColumn("dbo.Employers", "Name", c => c.String());
        }
    }
}
