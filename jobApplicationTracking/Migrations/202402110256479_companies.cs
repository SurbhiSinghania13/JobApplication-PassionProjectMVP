namespace jobApplicationTracking.Migrations
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
                    })
                .PrimaryKey(t => t.CompanyID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.companies");
        }
    }
}
