namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesignationSpecialProductLengh : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GlobalProperties", new[] { "DefaultProjectType_Id" });
            AlterColumn("dbo.Product", "DesignationSpecial", c => c.String(maxLength: 256));
            AlterColumn("dbo.GlobalProperties", "DefaultProjectType_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.GlobalProperties", "DefaultProjectType_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.GlobalProperties", new[] { "DefaultProjectType_Id" });
            AlterColumn("dbo.GlobalProperties", "DefaultProjectType_Id", c => c.Guid());
            AlterColumn("dbo.Product", "DesignationSpecial", c => c.String(maxLength: 50));
            CreateIndex("dbo.GlobalProperties", "DefaultProjectType_Id");
        }
    }
}
