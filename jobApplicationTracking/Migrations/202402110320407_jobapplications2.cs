namespace jobApplicationTracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplications2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.jobApplications", new[] { "CompanyId" });
            CreateIndex("dbo.jobApplications", "CompanyID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.jobApplications", new[] { "CompanyID" });
            CreateIndex("dbo.jobApplications", "CompanyId");
        }
    }
}
