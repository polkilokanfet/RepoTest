namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameFull = c.String(nullable: false, maxLength: 150),
                        NameShort = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategoryPriceAndCost",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Cost = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        StructureCost = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategory", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategoryParameter",
                c => new
                    {
                        ProductCategory_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCategory_Id, t.Parameter_Id })
                .ForeignKey("dbo.ProductCategory", t => t.ProductCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.ProductCategory_Id)
                .Index(t => t.Parameter_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCategoryPriceAndCost", "Id", "dbo.ProductCategory");
            DropForeignKey("dbo.ProductCategoryParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.ProductCategoryParameter", "ProductCategory_Id", "dbo.ProductCategory");
            DropIndex("dbo.ProductCategoryParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.ProductCategoryParameter", new[] { "ProductCategory_Id" });
            DropIndex("dbo.ProductCategoryPriceAndCost", new[] { "Id" });
            DropTable("dbo.ProductCategoryParameter");
            DropTable("dbo.ProductCategoryPriceAndCost");
            DropTable("dbo.ProductCategory");
        }
    }
}
