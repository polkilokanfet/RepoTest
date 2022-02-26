namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIsActualStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "IsActual", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "IsActual");
        }
    }
}
