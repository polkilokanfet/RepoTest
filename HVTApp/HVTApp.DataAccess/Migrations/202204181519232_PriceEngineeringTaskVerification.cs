namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTaskVerification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "RequestForVerificationFromHead", c => c.Boolean(nullable: false));
            AddColumn("dbo.PriceEngineeringTask", "RequestForVerificationFromConstructor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceEngineeringTask", "RequestForVerificationFromConstructor");
            DropColumn("dbo.PriceEngineeringTask", "RequestForVerificationFromHead");
        }
    }
}
