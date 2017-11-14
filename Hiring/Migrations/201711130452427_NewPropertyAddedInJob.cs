namespace Hiring.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPropertyAddedInJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "PublicationDate", c => c.String());
            AddColumn("dbo.Jobs", "Deadline", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Deadline");
            DropColumn("dbo.Jobs", "PublicationDate");
        }
    }
}
