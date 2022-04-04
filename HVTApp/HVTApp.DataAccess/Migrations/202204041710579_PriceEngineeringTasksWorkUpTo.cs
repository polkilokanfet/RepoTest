namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTasksWorkUpTo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTasks", "WorkUpTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PriceEngineeringTaskFileAnswer", "Comment", c => c.String(maxLength: 1024));
            AlterColumn("dbo.PriceEngineeringTaskFileTechnicalRequirements", "Comment", c => c.String(maxLength: 1024));
            AlterColumn("dbo.PriceEngineeringTasksFileTechnicalRequirements", "Comment", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PriceEngineeringTasksFileTechnicalRequirements", "Comment", c => c.String(nullable: false, maxLength: 1024));
            AlterColumn("dbo.PriceEngineeringTaskFileTechnicalRequirements", "Comment", c => c.String(nullable: false, maxLength: 1024));
            AlterColumn("dbo.PriceEngineeringTaskFileAnswer", "Comment", c => c.String(nullable: false, maxLength: 1024));
            DropColumn("dbo.PriceEngineeringTasks", "WorkUpTo");
        }
    }
}
