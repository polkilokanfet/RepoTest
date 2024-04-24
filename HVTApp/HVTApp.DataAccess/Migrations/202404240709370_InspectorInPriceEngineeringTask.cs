namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InspectorInPriceEngineeringTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "UserConstructorInspector_Id", c => c.Guid());
            CreateIndex("dbo.PriceEngineeringTask", "UserConstructorInspector_Id");
            AddForeignKey("dbo.PriceEngineeringTask", "UserConstructorInspector_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTask", "UserConstructorInspector_Id", "dbo.User");
            DropIndex("dbo.PriceEngineeringTask", new[] { "UserConstructorInspector_Id" });
            DropColumn("dbo.PriceEngineeringTask", "UserConstructorInspector_Id");
        }
    }
}
