namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTaskConfiguration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceEngineeringTaskFileAnswer", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskMessage", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskProductBlockAdded", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            AddForeignKey("dbo.PriceEngineeringTaskFileAnswer", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PriceEngineeringTaskMessage", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PriceEngineeringTaskProductBlockAdded", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTaskProductBlockAdded", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskMessage", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskFileAnswer", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            AddForeignKey("dbo.PriceEngineeringTaskProductBlockAdded", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id");
            AddForeignKey("dbo.PriceEngineeringTaskMessage", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id");
            AddForeignKey("dbo.PriceEngineeringTaskFileAnswer", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id");
        }
    }
}
