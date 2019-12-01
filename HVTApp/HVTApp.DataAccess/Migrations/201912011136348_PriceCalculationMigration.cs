namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceCalculation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskOpenMoment = c.DateTime(),
                        TaskCloseMoment = c.DateTime(),
                        Comment = c.String(maxLength: 200),
                        IsNeedExcelFile = c.Boolean(nullable: false),
                        Author_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Author_Id);
            
            AddColumn("dbo.SalesUnit", "PriceCalculation_Id", c => c.Guid());
            AddColumn("dbo.StructureCost", "UnitPrice", c => c.Double());
            AddColumn("dbo.StructureCost", "UnitPriceDate", c => c.DateTime());
            CreateIndex("dbo.SalesUnit", "PriceCalculation_Id");
            AddForeignKey("dbo.SalesUnit", "PriceCalculation_Id", "dbo.PriceCalculation", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesUnit", "PriceCalculation_Id", "dbo.PriceCalculation");
            DropForeignKey("dbo.PriceCalculation", "Author_Id", "dbo.User");
            DropIndex("dbo.SalesUnit", new[] { "PriceCalculation_Id" });
            DropIndex("dbo.PriceCalculation", new[] { "Author_Id" });
            DropColumn("dbo.StructureCost", "UnitPriceDate");
            DropColumn("dbo.StructureCost", "UnitPrice");
            DropColumn("dbo.SalesUnit", "PriceCalculation_Id");
            DropTable("dbo.PriceCalculation");
        }
    }
}
