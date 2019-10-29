namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureCostNumberChangeLengh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductBlock", "StructureCostNumber", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductBlock", "StructureCostNumber", c => c.String(maxLength: 10));
        }
    }
}
