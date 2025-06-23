namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KitDesignDepartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DesignDepartment", "IsKitDepartment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DesignDepartment", "IsKitDepartment");
        }
    }
}
