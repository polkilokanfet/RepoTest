namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStructureCosts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceCalculation", "Author_Id", "dbo.User");
            DropForeignKey("dbo.StructureCost", "StructureCosts_Id", "dbo.StructureCosts");
            DropForeignKey("dbo.SalesUnit", "StructureCosts_Id", "dbo.StructureCosts");
            DropForeignKey("dbo.PriceCalculationSalesUnit", "PriceCalculation_Id", "dbo.PriceCalculation");
            DropForeignKey("dbo.PriceCalculationSalesUnit", "SalesUnit_Id", "dbo.SalesUnit");
            DropIndex("dbo.PriceCalculation", new[] { "Author_Id" });
            DropIndex("dbo.SalesUnit", new[] { "StructureCosts_Id" });
            DropIndex("dbo.StructureCost", new[] { "StructureCosts_Id" });
            DropIndex("dbo.PriceCalculationSalesUnit", new[] { "PriceCalculation_Id" });
            DropIndex("dbo.PriceCalculationSalesUnit", new[] { "SalesUnit_Id" });
            CreateTable(
                "dbo.PriceCalculationItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceCalculationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceCalculation", t => t.PriceCalculationId, cascadeDelete: true)
                .Index(t => t.PriceCalculationId);
            
            CreateTable(
                "dbo.PriceCalculationItemSalesUnit",
                c => new
                    {
                        PriceCalculationItem_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PriceCalculationItem_Id, t.SalesUnit_Id })
                .ForeignKey("dbo.PriceCalculationItem", t => t.PriceCalculationItem_Id, cascadeDelete: true)
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .Index(t => t.PriceCalculationItem_Id)
                .Index(t => t.SalesUnit_Id);
            
            AddColumn("dbo.StructureCost", "PriceCalculationItemId", c => c.Guid(nullable: false));
            CreateIndex("dbo.StructureCost", "PriceCalculationItemId");
            AddForeignKey("dbo.StructureCost", "PriceCalculationItemId", "dbo.PriceCalculationItem", "Id", cascadeDelete: true);
            DropColumn("dbo.PriceCalculation", "Author_Id");
            DropColumn("dbo.SalesUnit", "StructureCosts_Id");
            DropColumn("dbo.StructureCost", "UnitPriceDate");
            DropColumn("dbo.StructureCost", "StructureCosts_Id");
            DropTable("dbo.StructureCosts");
            DropTable("dbo.PriceCalculationSalesUnit");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PriceCalculationSalesUnit",
                c => new
                    {
                        PriceCalculation_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PriceCalculation_Id, t.SalesUnit_Id });
            
            CreateTable(
                "dbo.StructureCosts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.StructureCost", "StructureCosts_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.StructureCost", "UnitPriceDate", c => c.DateTime());
            AddColumn("dbo.SalesUnit", "StructureCosts_Id", c => c.Guid());
            AddColumn("dbo.PriceCalculation", "Author_Id", c => c.Guid(nullable: false));
            DropForeignKey("dbo.PriceCalculationItem", "PriceCalculationId", "dbo.PriceCalculation");
            DropForeignKey("dbo.StructureCost", "PriceCalculationItemId", "dbo.PriceCalculationItem");
            DropForeignKey("dbo.PriceCalculationItemSalesUnit", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.PriceCalculationItemSalesUnit", "PriceCalculationItem_Id", "dbo.PriceCalculationItem");
            DropIndex("dbo.PriceCalculationItemSalesUnit", new[] { "SalesUnit_Id" });
            DropIndex("dbo.PriceCalculationItemSalesUnit", new[] { "PriceCalculationItem_Id" });
            DropIndex("dbo.StructureCost", new[] { "PriceCalculationItemId" });
            DropIndex("dbo.PriceCalculationItem", new[] { "PriceCalculationId" });
            DropColumn("dbo.StructureCost", "PriceCalculationItemId");
            DropTable("dbo.PriceCalculationItemSalesUnit");
            DropTable("dbo.PriceCalculationItem");
            CreateIndex("dbo.PriceCalculationSalesUnit", "SalesUnit_Id");
            CreateIndex("dbo.PriceCalculationSalesUnit", "PriceCalculation_Id");
            CreateIndex("dbo.StructureCost", "StructureCosts_Id");
            CreateIndex("dbo.SalesUnit", "StructureCosts_Id");
            CreateIndex("dbo.PriceCalculation", "Author_Id");
            AddForeignKey("dbo.PriceCalculationSalesUnit", "SalesUnit_Id", "dbo.SalesUnit", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PriceCalculationSalesUnit", "PriceCalculation_Id", "dbo.PriceCalculation", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SalesUnit", "StructureCosts_Id", "dbo.StructureCosts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StructureCost", "StructureCosts_Id", "dbo.StructureCosts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PriceCalculation", "Author_Id", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
