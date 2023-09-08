namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSccFromTask : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StructureCostVersion", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            AddForeignKey("dbo.StructureCostVersion", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StructureCostVersion", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            AddForeignKey("dbo.StructureCostVersion", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id");
        }
    }
}
