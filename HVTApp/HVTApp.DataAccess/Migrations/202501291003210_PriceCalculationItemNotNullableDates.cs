namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationItemNotNullableDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PriceCalculationItem", "OrderInTakeDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PriceCalculationItem", "RealizationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PriceCalculationItem", "RealizationDate", c => c.DateTime());
            AlterColumn("dbo.PriceCalculationItem", "OrderInTakeDate", c => c.DateTime());
        }
    }
}
