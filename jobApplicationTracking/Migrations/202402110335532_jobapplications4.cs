namespace jobApplicationTracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplications4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.jobApplications", "CompanyID", c => c.Int(nullable: false));
            CreateIndex("dbo.jobApplications", "CompanyID");
            AddForeignKey("dbo.jobApplications", "CompanyID", "dbo.companies", "CompanyID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobApplications", "CompanyID", "dbo.companies");
            DropIndex("dbo.jobApplications", new[] { "CompanyID" });
            DropColumn("dbo.jobApplications", "CompanyID");
        }
    }
}
