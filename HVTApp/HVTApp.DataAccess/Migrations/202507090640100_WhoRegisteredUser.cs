namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WhoRegisteredUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "WhoRegisteredUserId", c => c.Guid());
            CreateIndex("dbo.Document", "WhoRegisteredUserId");
            AddForeignKey("dbo.Document", "WhoRegisteredUserId", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Document", "WhoRegisteredUserId", "dbo.User");
            DropIndex("dbo.Document", new[] { "WhoRegisteredUserId" });
            DropColumn("dbo.Document", "WhoRegisteredUserId");
        }
    }
}
