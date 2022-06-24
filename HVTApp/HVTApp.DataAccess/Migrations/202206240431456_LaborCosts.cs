namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LaborCosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductBlock", "LaborCosts", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductBlock", "LaborCosts");
        }
    }
}
