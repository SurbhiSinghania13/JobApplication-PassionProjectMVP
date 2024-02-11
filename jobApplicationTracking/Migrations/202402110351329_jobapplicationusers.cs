namespace jobApplicationTracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplicationusers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserjobApplications",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        jobApplication_JobApplicationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.jobApplication_JobApplicationID })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.jobApplications", t => t.jobApplication_JobApplicationID, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.jobApplication_JobApplicationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserjobApplications", "jobApplication_JobApplicationID", "dbo.jobApplications");
            DropForeignKey("dbo.UserjobApplications", "User_UserId", "dbo.Users");
            DropIndex("dbo.UserjobApplications", new[] { "jobApplication_JobApplicationID" });
            DropIndex("dbo.UserjobApplications", new[] { "User_UserId" });
            DropTable("dbo.UserjobApplications");
        }
    }
}
