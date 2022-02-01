namespace JobPortal2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Active");
        }
    }
}
