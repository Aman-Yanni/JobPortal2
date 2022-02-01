namespace JobPortal2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ApplicationId = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                        userId_Id = c.String(maxLength: 128),
                        jobId_JobID = c.Int(),
                    })
                .PrimaryKey(t => t.ApplicationId)
                .ForeignKey("dbo.AspNetUsers", t => t.userId_Id)
                .ForeignKey("dbo.Jobs", t => t.jobId_JobID)
                .Index(t => t.userId_Id)
                .Index(t => t.jobId_JobID);
            
            AddColumn("dbo.Jobs", "Experience", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "degree", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "degree", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "experience", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "field", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "jobId_JobID", "dbo.Jobs");
            DropForeignKey("dbo.Applicants", "userId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Applicants", new[] { "jobId_JobID" });
            DropIndex("dbo.Applicants", new[] { "userId_Id" });
            DropColumn("dbo.AspNetUsers", "field");
            DropColumn("dbo.AspNetUsers", "experience");
            DropColumn("dbo.AspNetUsers", "degree");
            DropColumn("dbo.Jobs", "degree");
            DropColumn("dbo.Jobs", "Experience");
            DropTable("dbo.Applicants");
        }
    }
}
