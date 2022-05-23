namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTaskTce : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceEngineeringTaskTce",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TceNumber = c.String(maxLength: 12),
                        BackManager_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.BackManager_Id)
                .Index(t => t.BackManager_Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskTceStructureCostVersion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskTceId = c.Guid(nullable: false),
                        ParentUnitId = c.Guid(nullable: false),
                        StructureCostVersion = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTaskTce", t => t.PriceEngineeringTaskTceId)
                .Index(t => t.PriceEngineeringTaskTceId);
            
            CreateTable(
                "dbo.PriceEngineeringTaskTceStoryItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskTceId = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                        StoryAction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTaskTce", t => t.PriceEngineeringTaskTceId, cascadeDelete: true)
                .Index(t => t.PriceEngineeringTaskTceId);
            
            CreateTable(
                "dbo.PriceEngineeringTaskTcePriceEngineeringTask",
                c => new
                    {
                        PriceEngineeringTaskTce_Id = c.Guid(nullable: false),
                        PriceEngineeringTask_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PriceEngineeringTaskTce_Id, t.PriceEngineeringTask_Id })
                .ForeignKey("dbo.PriceEngineeringTaskTce", t => t.PriceEngineeringTaskTce_Id, cascadeDelete: true)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTask_Id, cascadeDelete: true)
                .Index(t => t.PriceEngineeringTaskTce_Id)
                .Index(t => t.PriceEngineeringTask_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTaskTceStoryItem", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce");
            DropForeignKey("dbo.PriceEngineeringTaskTceStructureCostVersion", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce");
            DropForeignKey("dbo.PriceEngineeringTaskTcePriceEngineeringTask", "PriceEngineeringTask_Id", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskTcePriceEngineeringTask", "PriceEngineeringTaskTce_Id", "dbo.PriceEngineeringTaskTce");
            DropForeignKey("dbo.PriceEngineeringTaskTce", "BackManager_Id", "dbo.User");
            DropIndex("dbo.PriceEngineeringTaskTcePriceEngineeringTask", new[] { "PriceEngineeringTask_Id" });
            DropIndex("dbo.PriceEngineeringTaskTcePriceEngineeringTask", new[] { "PriceEngineeringTaskTce_Id" });
            DropIndex("dbo.PriceEngineeringTaskTceStoryItem", new[] { "PriceEngineeringTaskTceId" });
            DropIndex("dbo.PriceEngineeringTaskTceStructureCostVersion", new[] { "PriceEngineeringTaskTceId" });
            DropIndex("dbo.PriceEngineeringTaskTce", new[] { "BackManager_Id" });
            DropTable("dbo.PriceEngineeringTaskTcePriceEngineeringTask");
            DropTable("dbo.PriceEngineeringTaskTceStoryItem");
            DropTable("dbo.PriceEngineeringTaskTceStructureCostVersion");
            DropTable("dbo.PriceEngineeringTaskTce");
        }
    }
}
