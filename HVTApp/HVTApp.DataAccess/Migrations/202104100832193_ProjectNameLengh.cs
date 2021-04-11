namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectNameLengh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlobalProperties", "DefaultProjectType_Id", c => c.Guid());
            AddColumn("dbo.TechnicalRequrementsTask", "LastOpenFrontManagerMoment", c => c.DateTime());
            AlterColumn("dbo.ProductBlock", "DesignationSpecial", c => c.String(maxLength: 256));
            AlterColumn("dbo.Project", "Name", c => c.String(nullable: false, maxLength: 512));
            CreateIndex("dbo.GlobalProperties", "DefaultProjectType_Id");
            AddForeignKey("dbo.GlobalProperties", "DefaultProjectType_Id", "dbo.ProjectType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlobalProperties", "DefaultProjectType_Id", "dbo.ProjectType");
            DropIndex("dbo.GlobalProperties", new[] { "DefaultProjectType_Id" });
            AlterColumn("dbo.Project", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ProductBlock", "DesignationSpecial", c => c.String(maxLength: 50));
            DropColumn("dbo.TechnicalRequrementsTask", "LastOpenFrontManagerMoment");
            DropColumn("dbo.GlobalProperties", "DefaultProjectType_Id");
        }
    }
}
