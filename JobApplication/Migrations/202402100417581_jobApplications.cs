namespace JobApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobApplications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.jobApplications",
                c => new
                    {
                        JobApplicationID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        CompanyName = c.String(),
                        Applicants = c.String(),
                    })
                .PrimaryKey(t => t.JobApplicationID);
            
            CreateTable(
                "dbo.jobApplicationUsers",
                c => new
                    {
                        jobApplication_JobApplicationID = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.jobApplication_JobApplicationID, t.User_UserId })
                .ForeignKey("dbo.jobApplications", t => t.jobApplication_JobApplicationID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.jobApplication_JobApplicationID)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobApplicationUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.jobApplicationUsers", "jobApplication_JobApplicationID", "dbo.jobApplications");
            DropIndex("dbo.jobApplicationUsers", new[] { "User_UserId" });
            DropIndex("dbo.jobApplicationUsers", new[] { "jobApplication_JobApplicationID" });
            DropTable("dbo.jobApplicationUsers");
            DropTable("dbo.jobApplications");
        }
    }
}
