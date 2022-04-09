namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentInTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceCalculation", "PriceEngineeringTasksId", c => c.Guid());
            AddColumn("dbo.PriceEngineeringTask", "DesignDepartment_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.PriceEngineeringTasks", "Comment", c => c.String(maxLength: 1024));
            CreateIndex("dbo.PriceCalculation", "PriceEngineeringTasksId");
            CreateIndex("dbo.PriceEngineeringTask", "DesignDepartment_Id");
            AddForeignKey("dbo.PriceEngineeringTask", "DesignDepartment_Id", "dbo.DesignDepartment", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PriceCalculation", "PriceEngineeringTasksId", "dbo.PriceEngineeringTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceCalculation", "PriceEngineeringTasksId", "dbo.PriceEngineeringTasks");
            DropForeignKey("dbo.PriceEngineeringTask", "DesignDepartment_Id", "dbo.DesignDepartment");
            DropIndex("dbo.PriceEngineeringTask", new[] { "DesignDepartment_Id" });
            DropIndex("dbo.PriceCalculation", new[] { "PriceEngineeringTasksId" });
            DropColumn("dbo.PriceEngineeringTasks", "Comment");
            DropColumn("dbo.PriceEngineeringTask", "DesignDepartment_Id");
            DropColumn("dbo.PriceCalculation", "PriceEngineeringTasksId");
        }
    }
}
