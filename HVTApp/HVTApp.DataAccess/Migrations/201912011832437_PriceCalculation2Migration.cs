namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculation2Migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SalesUnit", "PriceCalculation_Id", "dbo.PriceCalculation");
            DropIndex("dbo.SalesUnit", new[] { "PriceCalculation_Id" });
            CreateTable(
                "dbo.PriceCalculationSalesUnit",
                c => new
                    {
                        PriceCalculation_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PriceCalculation_Id, t.SalesUnit_Id })
                .ForeignKey("dbo.PriceCalculation", t => t.PriceCalculation_Id, cascadeDelete: true)
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .Index(t => t.PriceCalculation_Id)
                .Index(t => t.SalesUnit_Id);
            
            DropColumn("dbo.SalesUnit", "PriceCalculation_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesUnit", "PriceCalculation_Id", c => c.Guid());
            DropForeignKey("dbo.PriceCalculationSalesUnit", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.PriceCalculationSalesUnit", "PriceCalculation_Id", "dbo.PriceCalculation");
            DropIndex("dbo.PriceCalculationSalesUnit", new[] { "SalesUnit_Id" });
            DropIndex("dbo.PriceCalculationSalesUnit", new[] { "PriceCalculation_Id" });
            DropTable("dbo.PriceCalculationSalesUnit");
            CreateIndex("dbo.SalesUnit", "PriceCalculation_Id");
            AddForeignKey("dbo.SalesUnit", "PriceCalculation_Id", "dbo.PriceCalculation", "Id");
        }
    }
}
