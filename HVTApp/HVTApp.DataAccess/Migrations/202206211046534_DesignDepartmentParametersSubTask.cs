namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesignDepartmentParametersSubTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DesignDepartmentParametersSubTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DesignDepartmentId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DesignDepartment", t => t.DesignDepartmentId)
                .Index(t => t.DesignDepartmentId);
            
            CreateTable(
                "dbo.DesignDepartmentParametersSubTaskParameter",
                c => new
                    {
                        DesignDepartmentParametersSubTask_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DesignDepartmentParametersSubTask_Id, t.Parameter_Id })
                .ForeignKey("dbo.DesignDepartmentParametersSubTask", t => t.DesignDepartmentParametersSubTask_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.DesignDepartmentParametersSubTask_Id)
                .Index(t => t.Parameter_Id);
            
            AddColumn("dbo.PriceEngineeringTask", "UserConstructorInitiator_Id", c => c.Guid());
            CreateIndex("dbo.PriceEngineeringTask", "UserConstructorInitiator_Id");
            AddForeignKey("dbo.PriceEngineeringTask", "UserConstructorInitiator_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTask", "UserConstructorInitiator_Id", "dbo.User");
            DropForeignKey("dbo.DesignDepartmentParametersSubTask", "DesignDepartmentId", "dbo.DesignDepartment");
            DropForeignKey("dbo.DesignDepartmentParametersSubTaskParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.DesignDepartmentParametersSubTaskParameter", "DesignDepartmentParametersSubTask_Id", "dbo.DesignDepartmentParametersSubTask");
            DropIndex("dbo.DesignDepartmentParametersSubTaskParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.DesignDepartmentParametersSubTaskParameter", new[] { "DesignDepartmentParametersSubTask_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "UserConstructorInitiator_Id" });
            DropIndex("dbo.DesignDepartmentParametersSubTask", new[] { "DesignDepartmentId" });
            DropColumn("dbo.PriceEngineeringTask", "UserConstructorInitiator_Id");
            DropTable("dbo.DesignDepartmentParametersSubTaskParameter");
            DropTable("dbo.DesignDepartmentParametersSubTask");
        }
    }
}
