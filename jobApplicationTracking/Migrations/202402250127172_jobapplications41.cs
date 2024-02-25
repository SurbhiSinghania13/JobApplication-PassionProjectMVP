namespace jobApplicationTracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplications41 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.jobApplications", "CompanyName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.jobApplications", "CompanyName", c => c.String());
        }
    }
}
