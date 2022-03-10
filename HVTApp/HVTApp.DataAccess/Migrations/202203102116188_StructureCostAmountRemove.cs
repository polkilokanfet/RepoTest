namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureCostAmountRemove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StructureCost", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StructureCost", "Amount", c => c.Double(nullable: false));
        }
    }
}
