namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationSettingsFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PaymentConditionSet");
            DropIndex("dbo.PriceCalculationSettings", new[] { "Id" });
            AddColumn("dbo.PriceCalculationSettings", "PaymentConditionSet_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PriceCalculationSettings", "PaymentConditionSet_Id");
            AddForeignKey("dbo.PriceCalculationSettings", "PaymentConditionSet_Id", "dbo.PaymentConditionSet", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceCalculationSettings", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropIndex("dbo.PriceCalculationSettings", new[] { "PaymentConditionSet_Id" });
            DropColumn("dbo.PriceCalculationSettings", "PaymentConditionSet_Id");
            CreateIndex("dbo.PriceCalculationSettings", "Id");
            AddForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PaymentConditionSet", "Id");
        }
    }
}
