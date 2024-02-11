namespace jobApplicationTracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplicationcompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.jobApplications", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.jobApplications", "CompanyId");
            AddForeignKey("dbo.jobApplications", "CompanyId", "dbo.companies", "CompanyID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobApplications", "CompanyId", "dbo.companies");
            DropIndex("dbo.jobApplications", new[] { "CompanyId" });
            DropColumn("dbo.jobApplications", "CompanyId");
        }
    }
}
