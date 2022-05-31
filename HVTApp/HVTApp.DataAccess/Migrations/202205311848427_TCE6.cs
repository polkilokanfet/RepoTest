namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TCE6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.PriceCalculationSettings", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropIndex("dbo.PriceCalculationItem", new[] { "PaymentConditionSet_Id" });
            DropIndex("dbo.PriceCalculationSettings", new[] { "Id" });
            DropIndex("dbo.PriceCalculationSettings", new[] { "PriceEngineeringTaskId" });
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
            
            AddColumn("dbo.PriceCalculation", "IsTceConnected", c => c.Boolean(nullable: false));
            AddColumn("dbo.PriceCalculationItem", "PriceEngineeringTaskId", c => c.Guid());
            AddColumn("dbo.StructureCost", "OriginalStructureCostNumber", c => c.String(maxLength: 50));
            AddColumn("dbo.StructureCost", "OriginalStructureCostProductBlock_Id", c => c.Guid());
            AddColumn("dbo.PriceEngineeringTaskProductBlockAdded", "IsRemoved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PriceEngineeringTasks", "TceNumber", c => c.String(maxLength: 12));
            AddColumn("dbo.PriceEngineeringTasks", "BackManager_Id", c => c.Guid());
            AlterColumn("dbo.PriceCalculationItem", "PaymentConditionSet_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PriceCalculationItem", "PriceEngineeringTaskId");
            CreateIndex("dbo.PriceCalculationItem", "PaymentConditionSet_Id");
            CreateIndex("dbo.StructureCost", "OriginalStructureCostProductBlock_Id");
            CreateIndex("dbo.PriceEngineeringTasks", "BackManager_Id");
            AddForeignKey("dbo.StructureCost", "OriginalStructureCostProductBlock_Id", "dbo.ProductBlock", "Id");
            AddForeignKey("dbo.PriceCalculationItem", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id");
            AddForeignKey("dbo.PriceEngineeringTasks", "BackManager_Id", "dbo.User", "Id");
            DropTable("dbo.PriceCalculationSettings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PriceCalculationSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(nullable: false),
                        StartMoment = c.DateTime(nullable: false),
                        DateOrderInTake = c.DateTime(nullable: false),
                        DateRealization = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.PriceEngineeringTasks", "BackManager_Id", "dbo.User");
            DropForeignKey("dbo.StructureCostVersion", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.StructureCostVersion", "PriceEngineeringTaskProductBlockAddedId", "dbo.PriceEngineeringTaskProductBlockAdded");
            DropForeignKey("dbo.PriceCalculationItem", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.StructureCost", "OriginalStructureCostProductBlock_Id", "dbo.ProductBlock");
            DropIndex("dbo.PriceEngineeringTasks", new[] { "BackManager_Id" });
            DropIndex("dbo.StructureCostVersion", new[] { "PriceEngineeringTaskProductBlockAddedId" });
            DropIndex("dbo.StructureCostVersion", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.StructureCost", new[] { "OriginalStructureCostProductBlock_Id" });
            DropIndex("dbo.PriceCalculationItem", new[] { "PaymentConditionSet_Id" });
            DropIndex("dbo.PriceCalculationItem", new[] { "PriceEngineeringTaskId" });
            AlterColumn("dbo.PriceCalculationItem", "PaymentConditionSet_Id", c => c.Guid());
            DropColumn("dbo.PriceEngineeringTasks", "BackManager_Id");
            DropColumn("dbo.PriceEngineeringTasks", "TceNumber");
            DropColumn("dbo.PriceEngineeringTaskProductBlockAdded", "IsRemoved");
            DropColumn("dbo.StructureCost", "OriginalStructureCostProductBlock_Id");
            DropColumn("dbo.StructureCost", "OriginalStructureCostNumber");
            DropColumn("dbo.PriceCalculationItem", "PriceEngineeringTaskId");
            DropColumn("dbo.PriceCalculation", "IsTceConnected");
            DropTable("dbo.StructureCostVersion");
            CreateIndex("dbo.PriceCalculationSettings", "PriceEngineeringTaskId");
            CreateIndex("dbo.PriceCalculationSettings", "Id");
            CreateIndex("dbo.PriceCalculationItem", "PaymentConditionSet_Id");
            AddForeignKey("dbo.PriceCalculationSettings", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id");
            AddForeignKey("dbo.PriceCalculationSettings", "Id", "dbo.PaymentConditionSet", "Id");
        }
    }
}
