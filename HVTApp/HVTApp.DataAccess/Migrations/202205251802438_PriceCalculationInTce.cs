namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationInTce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceCalculation", "PriceEngineeringTaskTceId", c => c.Guid());
            CreateIndex("dbo.PriceCalculation", "PriceEngineeringTaskTceId");
            AddForeignKey("dbo.PriceCalculation", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceCalculation", "PriceEngineeringTaskTceId", "dbo.PriceEngineeringTaskTce");
            DropIndex("dbo.PriceCalculation", new[] { "PriceEngineeringTaskTceId" });
            DropColumn("dbo.PriceCalculation", "PriceEngineeringTaskTceId");
        }
    }
}
