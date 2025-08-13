namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureCostNumberIsRequired : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductBlock", "StructureCostNumberIsRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductBlock", "StructureCostNumberIsRequired");
        }
    }
}
