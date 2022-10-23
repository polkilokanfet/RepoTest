namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTaskNumbering : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "Number", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.PriceEngineeringTasks", "Number", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceEngineeringTasks", "Number");
            DropColumn("dbo.PriceEngineeringTask", "Number");
        }
    }
}
