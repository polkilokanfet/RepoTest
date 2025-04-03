namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostWithReserve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesUnit", "CostWithReserve", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesUnit", "CostWithReserve");
        }
    }
}
