namespace JobApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.companies",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Industry = c.String(),
                        CompanyLocation = c.String(),
                        CompanyWebsite = c.String(),
                        JobsAvailable = c.String(),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            AddColumn("dbo.jobApplications", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.jobApplications", "CompanyId");
            AddForeignKey("dbo.jobApplications", "CompanyId", "dbo.companies", "CompanyID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobApplications", "CompanyId", "dbo.companies");
            DropIndex("dbo.jobApplications", new[] { "CompanyId" });
            DropColumn("dbo.jobApplications", "CompanyId");
            DropTable("dbo.companies");
        }
    }
}
