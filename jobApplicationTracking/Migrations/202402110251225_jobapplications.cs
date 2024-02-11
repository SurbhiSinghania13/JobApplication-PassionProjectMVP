namespace jobApplicationTracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplications : DbMigration
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
                        JobLocation = c.String(),
                    })
                .PrimaryKey(t => t.JobApplicationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.jobApplications");
        }
    }
}
