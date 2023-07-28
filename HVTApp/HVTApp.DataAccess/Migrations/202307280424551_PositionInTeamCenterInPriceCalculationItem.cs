namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PositionInTeamCenterInPriceCalculationItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceCalculationItem", "PositionInTeamCenter", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceCalculationItem", "PositionInTeamCenter");
        }
    }
}
