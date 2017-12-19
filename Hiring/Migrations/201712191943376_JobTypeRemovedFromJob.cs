namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobTypeRemovedFromJob : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "JobType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "JobType", c => c.Int(nullable: false));
        }
    }
}
