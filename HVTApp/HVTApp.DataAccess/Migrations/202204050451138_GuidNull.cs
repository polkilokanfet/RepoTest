namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuidNull : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PriceEngineeringTask", new[] { "ParentPriceEngineeringTasksId" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ParentPriceEngineeringTaskId" });
            AlterColumn("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId", c => c.Guid());
            AlterColumn("dbo.PriceEngineeringTask", "ParentPriceEngineeringTaskId", c => c.Guid());
            CreateIndex("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId");
            CreateIndex("dbo.PriceEngineeringTask", "ParentPriceEngineeringTaskId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PriceEngineeringTask", new[] { "ParentPriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ParentPriceEngineeringTasksId" });
            AlterColumn("dbo.PriceEngineeringTask", "ParentPriceEngineeringTaskId", c => c.Guid(nullable: false));
            AlterColumn("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PriceEngineeringTask", "ParentPriceEngineeringTaskId");
            CreateIndex("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId");
        }
    }
}
