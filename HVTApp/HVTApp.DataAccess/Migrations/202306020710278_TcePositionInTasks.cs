namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TcePositionInTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "TcePosition", c => c.String(maxLength: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceEngineeringTask", "TcePosition");
        }
    }
}
