namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlockAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTaskProductBlockAdded", "IsOnBlock", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UserRole", "Name", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserRole", "Name", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.PriceEngineeringTaskProductBlockAdded", "IsOnBlock");
        }
    }
}
