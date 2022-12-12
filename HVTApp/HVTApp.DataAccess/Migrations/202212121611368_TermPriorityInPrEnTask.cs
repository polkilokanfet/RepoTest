namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TermPriorityInPrEnTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "TermPriority", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceEngineeringTask", "TermPriority");
        }
    }
}
