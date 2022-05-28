namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceCalculationSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceCalculationSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateOrderInTake = c.DateTime(nullable: false),
                        DateRealization = c.DateTime(nullable: false),
                        PaymentConditionSet_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentConditionSet", t => t.PaymentConditionSet_Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.PaymentConditionSet_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceCalculationSettings", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropIndex("dbo.PriceCalculationSettings", new[] { "PaymentConditionSet_Id" });
            DropIndex("dbo.PriceCalculationSettings", new[] { "Id" });
            DropTable("dbo.PriceCalculationSettings");
        }
    }
}
