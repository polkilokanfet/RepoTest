namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationHistoryItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceCalculationHistoryItem", "User_Id", c => c.Guid());
            CreateIndex("dbo.PriceCalculationHistoryItem", "User_Id");
            AddForeignKey("dbo.PriceCalculationHistoryItem", "User_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceCalculationHistoryItem", "User_Id", "dbo.User");
            DropIndex("dbo.PriceCalculationHistoryItem", new[] { "User_Id" });
            DropColumn("dbo.PriceCalculationHistoryItem", "User_Id");
        }
    }
}
