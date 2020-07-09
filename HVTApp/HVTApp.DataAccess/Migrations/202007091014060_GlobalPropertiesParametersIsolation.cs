namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlobalPropertiesParametersIsolation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlobalProperties", "IsolationColorGroup_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.GlobalProperties", "IsolationDpuGroup_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.GlobalProperties", "IsolationMaterialGroup_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.Tender", "Link", c => c.String(nullable: false));
            CreateIndex("dbo.GlobalProperties", "IsolationColorGroup_Id");
            CreateIndex("dbo.GlobalProperties", "IsolationDpuGroup_Id");
            CreateIndex("dbo.GlobalProperties", "IsolationMaterialGroup_Id");
            AddForeignKey("dbo.GlobalProperties", "IsolationColorGroup_Id", "dbo.ParameterGroup", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GlobalProperties", "IsolationDpuGroup_Id", "dbo.ParameterGroup", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GlobalProperties", "IsolationMaterialGroup_Id", "dbo.ParameterGroup", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlobalProperties", "IsolationMaterialGroup_Id", "dbo.ParameterGroup");
            DropForeignKey("dbo.GlobalProperties", "IsolationDpuGroup_Id", "dbo.ParameterGroup");
            DropForeignKey("dbo.GlobalProperties", "IsolationColorGroup_Id", "dbo.ParameterGroup");
            DropIndex("dbo.GlobalProperties", new[] { "IsolationMaterialGroup_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "IsolationDpuGroup_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "IsolationColorGroup_Id" });
            DropColumn("dbo.Tender", "Link");
            DropColumn("dbo.GlobalProperties", "IsolationMaterialGroup_Id");
            DropColumn("dbo.GlobalProperties", "IsolationDpuGroup_Id");
            DropColumn("dbo.GlobalProperties", "IsolationColorGroup_Id");
        }
    }
}
