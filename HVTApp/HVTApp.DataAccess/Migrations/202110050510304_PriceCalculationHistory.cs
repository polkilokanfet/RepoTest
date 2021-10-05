namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceCalculationHistoryItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceCalculationId = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Comment = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceCalculation", t => t.PriceCalculationId, cascadeDelete: true)
                .Index(t => t.PriceCalculationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceCalculationHistoryItem", "PriceCalculationId", "dbo.PriceCalculation");
            DropIndex("dbo.PriceCalculationHistoryItem", new[] { "PriceCalculationId" });
            DropTable("dbo.PriceCalculationHistoryItem");
        }
    }
}
