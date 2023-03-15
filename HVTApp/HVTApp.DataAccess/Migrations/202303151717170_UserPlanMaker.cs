namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPlanMaker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "UserPlanMaker_Id", c => c.Guid());
            CreateIndex("dbo.PriceEngineeringTask", "UserPlanMaker_Id");
            AddForeignKey("dbo.PriceEngineeringTask", "UserPlanMaker_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTask", "UserPlanMaker_Id", "dbo.User");
            DropIndex("dbo.PriceEngineeringTask", new[] { "UserPlanMaker_Id" });
            DropColumn("dbo.PriceEngineeringTask", "UserPlanMaker_Id");
        }
    }
}
