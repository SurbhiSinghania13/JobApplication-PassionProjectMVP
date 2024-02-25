namespace jobApplicationTracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplications43 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.jobApplications", "CompanyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.jobApplications", "CompanyName");
        }
    }
}
