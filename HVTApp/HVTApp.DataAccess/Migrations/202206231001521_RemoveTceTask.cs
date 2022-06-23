namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTceTask : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceEngineeringTaskTce", "BackManager_Id", "dbo.User");
            DropForeignKey("dbo.PriceCalculation", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce");
            DropForeignKey("dbo.PriceEngineeringTaskTcePriceEngineeringTask", "PriceEngineeringTaskTce_Id", "dbo.PriceEngineeringTaskTce");
            DropForeignKey("dbo.PriceEngineeringTaskTcePriceEngineeringTask", "PriceEngineeringTask_Id", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskTceStructureCostVersion", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce");
            DropForeignKey("dbo.PriceEngineeringTaskTceStoryItem", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce");
            DropIndex("dbo.PriceCalculation", new[] { "PriceEngineeringTaskTceId" });
            DropIndex("dbo.PriceEngineeringTaskTce", new[] { "BackManager_Id" });
            DropIndex("dbo.PriceEngineeringTaskTceStructureCostVersion", new[] { "PriceEngineeringTaskTceId" });
            DropIndex("dbo.PriceEngineeringTaskTceStoryItem", new[] { "PriceEngineeringTaskTceId" });
            DropIndex("dbo.PriceEngineeringTaskTcePriceEngineeringTask", new[] { "PriceEngineeringTaskTce_Id" });
            DropIndex("dbo.PriceEngineeringTaskTcePriceEngineeringTask", new[] { "PriceEngineeringTask_Id" });
            DropColumn("dbo.PriceCalculation", "PriceEngineeringTaskTceId");
            DropTable("dbo.PriceEngineeringTaskTce");
            DropTable("dbo.PriceEngineeringTaskTceStructureCostVersion");
            DropTable("dbo.PriceEngineeringTaskTceStoryItem");
            DropTable("dbo.PriceEngineeringTaskTcePriceEngineeringTask");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PriceEngineeringTaskTcePriceEngineeringTask",
                c => new
                    {
                        PriceEngineeringTaskTce_Id = c.Guid(nullable: false),
                        PriceEngineeringTask_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PriceEngineeringTaskTce_Id, t.PriceEngineeringTask_Id });
            
            CreateTable(
                "dbo.PriceEngineeringTaskTceStoryItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskTceId = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                        StoryAction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskTceStructureCostVersion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskTceId = c.Guid(nullable: false),
                        ParentUnitId = c.Guid(nullable: false),
                        StructureCostVersion = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskTce",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TceNumber = c.String(maxLength: 12),
                        BackManager_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PriceCalculation", "PriceEngineeringTaskTceId", c => c.Guid());
            CreateIndex("dbo.PriceEngineeringTaskTcePriceEngineeringTask", "PriceEngineeringTask_Id");
            CreateIndex("dbo.PriceEngineeringTaskTcePriceEngineeringTask", "PriceEngineeringTaskTce_Id");
            CreateIndex("dbo.PriceEngineeringTaskTceStoryItem", "PriceEngineeringTaskTceId");
            CreateIndex("dbo.PriceEngineeringTaskTceStructureCostVersion", "PriceEngineeringTaskTceId");
            CreateIndex("dbo.PriceEngineeringTaskTce", "BackManager_Id");
            CreateIndex("dbo.PriceCalculation", "PriceEngineeringTaskTceId");
            AddForeignKey("dbo.PriceEngineeringTaskTceStoryItem", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PriceEngineeringTaskTceStructureCostVersion", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce", "Id");
            AddForeignKey("dbo.PriceEngineeringTaskTcePriceEngineeringTask", "PriceEngineeringTask_Id", "dbo.PriceEngineeringTask", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PriceEngineeringTaskTcePriceEngineeringTask", "PriceEngineeringTaskTce_Id", "dbo.PriceEngineeringTaskTce", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PriceCalculation", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce", "Id");
            AddForeignKey("dbo.PriceEngineeringTaskTce", "BackManager_Id", "dbo.User", "Id");
        }
    }
}
