namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationSettingsFix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PaymentConditionSet");
            DropIndex("dbo.PriceCalculationSettings", new[] { "PriceEngineeringTaskId" });
            DropColumn("dbo.PriceCalculationSettings", "Id");
            RenameColumn(table: "dbo.PriceCalculationSettings", name: "PriceEngineeringTaskId", newName: "Id");
            AddColumn("dbo.PriceCalculationSettings", "PaymentConditionSet_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PriceCalculationSettings", "PaymentConditionSet_Id");
            AddForeignKey("dbo.PriceCalculationSettings", "PaymentConditionSet_Id", "dbo.PaymentConditionSet", "Id");
            DropColumn("dbo.PriceCalculationSettings", "StartMoment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceCalculationSettings", "StartMoment", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.PriceCalculationSettings", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropIndex("dbo.PriceCalculationSettings", new[] { "PaymentConditionSet_Id" });
            DropColumn("dbo.PriceCalculationSettings", "PaymentConditionSet_Id");
            RenameColumn(table: "dbo.PriceCalculationSettings", name: "Id", newName: "PriceEngineeringTaskId");
            AddColumn("dbo.PriceCalculationSettings", "Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PriceCalculationSettings", "PriceEngineeringTaskId");
            AddForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PaymentConditionSet", "Id");
        }
    }
}
