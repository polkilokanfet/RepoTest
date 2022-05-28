namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationTaskSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceCalculationTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceCalculationTaskSetting",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceCalculationTaskId = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(nullable: false),
                        DateOrderInTake = c.DateTime(nullable: false),
                        DateRealization = c.DateTime(nullable: false),
                        PaymentConditionSet_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentConditionSet", t => t.PaymentConditionSet_Id)
                .ForeignKey("dbo.PriceCalculationTask", t => t.PriceCalculationTaskId)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTaskId)
                .Index(t => t.PriceCalculationTaskId)
                .Index(t => t.PriceEngineeringTaskId)
                .Index(t => t.PaymentConditionSet_Id);
            
            CreateTable(
                "dbo.StructureCostVersion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(),
                        PriceEngineeringTaskProductBlockAddedId = c.Guid(),
                        OriginalStructureCostNumber = c.String(maxLength: 50),
                        Version = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTaskProductBlockAdded", t => t.PriceEngineeringTaskProductBlockAddedId)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTaskId)
                .Index(t => t.PriceEngineeringTaskId)
                .Index(t => t.PriceEngineeringTaskProductBlockAddedId);
            
            AddColumn("dbo.PriceEngineeringTaskProductBlockAdded", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PriceEngineeringTasks", "TceNumber", c => c.String(maxLength: 12));
            AddColumn("dbo.PriceEngineeringTasks", "BackManager_Id", c => c.Guid());
            CreateIndex("dbo.PriceEngineeringTasks", "BackManager_Id");
            AddForeignKey("dbo.PriceEngineeringTasks", "BackManager_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTasks", "BackManager_Id", "dbo.User");
            DropForeignKey("dbo.StructureCostVersion", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.StructureCostVersion", "PriceEngineeringTaskProductBlockAddedId", "dbo.PriceEngineeringTaskProductBlockAdded");
            DropForeignKey("dbo.PriceCalculationTaskSetting", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceCalculationTaskSetting", "PriceCalculationTaskId", "dbo.PriceCalculationTask");
            DropForeignKey("dbo.PriceCalculationTaskSetting", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropIndex("dbo.PriceEngineeringTasks", new[] { "BackManager_Id" });
            DropIndex("dbo.StructureCostVersion", new[] { "PriceEngineeringTaskProductBlockAddedId" });
            DropIndex("dbo.StructureCostVersion", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceCalculationTaskSetting", new[] { "PaymentConditionSet_Id" });
            DropIndex("dbo.PriceCalculationTaskSetting", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceCalculationTaskSetting", new[] { "PriceCalculationTaskId" });
            DropColumn("dbo.PriceEngineeringTasks", "BackManager_Id");
            DropColumn("dbo.PriceEngineeringTasks", "TceNumber");
            DropColumn("dbo.PriceEngineeringTaskProductBlockAdded", "IsRemoved");
            DropTable("dbo.StructureCostVersion");
            DropTable("dbo.PriceCalculationTaskSetting");
            DropTable("dbo.PriceCalculationTask");
        }
    }
}
