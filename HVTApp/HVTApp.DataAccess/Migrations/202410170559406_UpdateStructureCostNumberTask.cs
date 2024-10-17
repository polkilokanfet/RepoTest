namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStructureCostNumberTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UpdateStructureCostNumberTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MomentStart = c.DateTime(nullable: false),
                        MomentFinish = c.DateTime(),
                        StructureCostNumberOriginal = c.String(nullable: false, maxLength: 50),
                        StructureCostNumber = c.String(nullable: false, maxLength: 50),
                        IsAccepted = c.Boolean(),
                        ProductBlock_Id = c.Guid(nullable: false),
                        PriceEngineeringTask_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlock_Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTask_Id)
                .Index(t => t.ProductBlock_Id)
                .Index(t => t.PriceEngineeringTask_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UpdateStructureCostNumberTask", "PriceEngineeringTask_Id", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.UpdateStructureCostNumberTask", "ProductBlock_Id", "dbo.ProductBlock");
            DropIndex("dbo.UpdateStructureCostNumberTask", new[] { "PriceEngineeringTask_Id" });
            DropIndex("dbo.UpdateStructureCostNumberTask", new[] { "ProductBlock_Id" });
            DropTable("dbo.UpdateStructureCostNumberTask");
        }
    }
}
