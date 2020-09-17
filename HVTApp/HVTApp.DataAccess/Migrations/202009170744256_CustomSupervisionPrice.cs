namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomSupervisionPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductIncluded", "CustomFixedPrice", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductIncluded", "CustomFixedPrice");
        }
    }
}
