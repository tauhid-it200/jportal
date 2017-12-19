namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressAddedInEmployerModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "JobType", c => c.Int(nullable: false));
            AddColumn("dbo.Employers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employers", "Address");
            DropColumn("dbo.Jobs", "JobType");
        }
    }
}
