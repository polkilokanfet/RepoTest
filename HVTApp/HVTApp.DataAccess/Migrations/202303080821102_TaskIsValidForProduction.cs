namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskIsValidForProduction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "IsValidForProduction", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceEngineeringTask", "IsValidForProduction");
        }
    }
}
