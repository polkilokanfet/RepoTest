namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationHistoryRemoveDates : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PriceCalculation", "TaskOpenMoment");
            DropColumn("dbo.PriceCalculation", "TaskCloseMoment");
            DropColumn("dbo.PriceCalculation", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceCalculation", "Comment", c => c.String(maxLength: 200));
            AddColumn("dbo.PriceCalculation", "TaskCloseMoment", c => c.DateTime());
            AddColumn("dbo.PriceCalculation", "TaskOpenMoment", c => c.DateTime());
        }
    }
}
