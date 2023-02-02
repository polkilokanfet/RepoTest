namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTaskConfigurationStatuses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceEngineeringTaskStatus", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            AddForeignKey("dbo.PriceEngineeringTaskStatus", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTaskStatus", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            AddForeignKey("dbo.PriceEngineeringTaskStatus", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id");
        }
    }
}
