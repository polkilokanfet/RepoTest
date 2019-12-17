namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17122019 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceCalculationItem", "OrderInTakeDate", c => c.DateTime());
            AddColumn("dbo.PriceCalculationItem", "RealizationDate", c => c.DateTime());
            AddColumn("dbo.PriceCalculationItem", "PaymentConditionSet_Id", c => c.Guid());
            AlterColumn("dbo.CreateNewProductTask", "Designation", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.CreateNewProductTask", "StructureCostNumber", c => c.String(nullable: false, maxLength: 25));
            CreateIndex("dbo.PriceCalculationItem", "PaymentConditionSet_Id");
            AddForeignKey("dbo.PriceCalculationItem", "PaymentConditionSet_Id", "dbo.PaymentConditionSet", "Id");
            DropColumn("dbo.SalesUnit", "TceRequest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesUnit", "TceRequest", c => c.String(maxLength: 20));
            DropForeignKey("dbo.PriceCalculationItem", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropIndex("dbo.PriceCalculationItem", new[] { "PaymentConditionSet_Id" });
            AlterColumn("dbo.CreateNewProductTask", "StructureCostNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.CreateNewProductTask", "Designation", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.PriceCalculationItem", "PaymentConditionSet_Id");
            DropColumn("dbo.PriceCalculationItem", "RealizationDate");
            DropColumn("dbo.PriceCalculationItem", "OrderInTakeDate");
        }
    }
}
