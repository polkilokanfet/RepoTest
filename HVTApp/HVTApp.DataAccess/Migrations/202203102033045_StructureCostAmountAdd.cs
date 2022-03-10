namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureCostAmountAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StructureCost", "AmountNumerator", c => c.Double(nullable: false));
            AddColumn("dbo.StructureCost", "AmountDenomerator", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StructureCost", "AmountDenomerator");
            DropColumn("dbo.StructureCost", "AmountNumerator");
        }
    }
}
