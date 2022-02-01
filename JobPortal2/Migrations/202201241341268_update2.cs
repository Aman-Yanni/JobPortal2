namespace JobPortal2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Jobs", name: "ApplicationUser_Id", newName: "UserID_Id");
            RenameIndex(table: "dbo.Jobs", name: "IX_ApplicationUser_Id", newName: "IX_UserID_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Jobs", name: "IX_UserID_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Jobs", name: "UserID_Id", newName: "ApplicationUser_Id");
        }
    }
}
