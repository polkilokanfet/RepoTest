namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPositionInTeamCenterInTRT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TechnicalRequrements", "PositionInTeamCenter", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TechnicalRequrements", "PositionInTeamCenter");
        }
    }
}
