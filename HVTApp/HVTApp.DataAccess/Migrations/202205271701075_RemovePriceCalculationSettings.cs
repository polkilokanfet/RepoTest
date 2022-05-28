namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePriceCalculationSettings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.PriceCalculationSettings", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropIndex("dbo.PriceCalculationSettings", new[] { "Id" });
            DropIndex("dbo.PriceCalculationSettings", new[] { "PriceEngineeringTaskId" });
            DropTable("dbo.PriceCalculationSettings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PriceCalculationSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(nullable: false),
                        StartMoment = c.DateTime(nullable: false),
                        DateOrderInTake = c.DateTime(nullable: false),
                        DateRealization = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.PriceCalculationSettings", "PriceEngineeringTaskId");
            CreateIndex("dbo.PriceCalculationSettings", "Id");
            AddForeignKey("dbo.PriceCalculationSettings", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id");
            AddForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PaymentConditionSet", "Id");
        }
    }
}
