namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationSettings : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentConditionSet", t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTaskId)
                .Index(t => t.Id)
                .Index(t => t.PriceEngineeringTaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceCalculationSettings", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PaymentConditionSet");
            DropIndex("dbo.PriceCalculationSettings", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceCalculationSettings", new[] { "Id" });
            DropTable("dbo.PriceCalculationSettings");
        }
    }
}
