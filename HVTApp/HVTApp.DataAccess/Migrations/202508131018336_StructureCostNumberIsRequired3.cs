namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureCostNumberIsRequired3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProductIncluded", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.OfferUnit", name: "Product_Id", newName: "ProductId");
            RenameIndex(table: "dbo.ProductIncluded", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameIndex(table: "dbo.OfferUnit", name: "IX_Product_Id", newName: "IX_ProductId");
            AddColumn("dbo.ProductBlock", "StructureCostNumberIsRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductBlock", "StructureCostNumberIsRequired");
            RenameIndex(table: "dbo.OfferUnit", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameIndex(table: "dbo.ProductIncluded", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameColumn(table: "dbo.OfferUnit", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.ProductIncluded", name: "ProductId", newName: "Product_Id");
        }
    }
}
