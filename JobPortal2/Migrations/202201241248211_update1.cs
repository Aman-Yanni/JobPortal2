namespace JobPortal2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Jobs", "ApplicationUser_Id");
            AddForeignKey("dbo.Jobs", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Jobs", "ApplicationUser_Id");
        }
    }
}
