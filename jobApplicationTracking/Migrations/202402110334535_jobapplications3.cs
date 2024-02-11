namespace jobApplicationTracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplications3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.jobApplications", "CompanyID", "dbo.companies");
            DropIndex("dbo.jobApplications", new[] { "CompanyID" });
            DropColumn("dbo.jobApplications", "CompanyID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.jobApplications", "CompanyID", c => c.Int(nullable: false));
            CreateIndex("dbo.jobApplications", "CompanyID");
            AddForeignKey("dbo.jobApplications", "CompanyID", "dbo.companies", "CompanyID", cascadeDelete: true);
        }
    }
}
