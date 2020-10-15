namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixBackManagerBug : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.User", new[] { "TechnicalRequrementsTask_Id" });
            RenameColumn(table: "dbo.TechnicalRequrementsTask", name: "TechnicalRequrementsTask_Id", newName: "BackManager_Id");
            CreateIndex("dbo.TechnicalRequrementsTask", "BackManager_Id");
            DropColumn("dbo.User", "TechnicalRequrementsTask_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "TechnicalRequrementsTask_Id", c => c.Guid());
            DropIndex("dbo.TechnicalRequrementsTask", new[] { "BackManager_Id" });
            RenameColumn(table: "dbo.TechnicalRequrementsTask", name: "BackManager_Id", newName: "TechnicalRequrementsTask_Id");
            CreateIndex("dbo.User", "TechnicalRequrementsTask_Id");
        }
    }
}
