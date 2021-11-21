namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TCEOrderInTakeDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TechnicalRequrements", "OrderInTakeDate", c => c.DateTime());
            AddColumn("dbo.TechnicalRequrements", "RealizationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TechnicalRequrements", "RealizationDate");
            DropColumn("dbo.TechnicalRequrements", "OrderInTakeDate");
        }
    }
}
