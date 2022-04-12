namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineering : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DesignDepartment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 120),
                        Head_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Head_Id)
                .Index(t => t.Head_Id);
            
            CreateTable(
                "dbo.DesignDepartmentParameters",
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
                "dbo.DesignDepartmentParametersAddedBlocks",
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
                "dbo.PriceEngineeringTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentPriceEngineeringTasksId = c.Guid(),
                        Amount = c.Int(nullable: false),
                        ParentPriceEngineeringTaskId = c.Guid(),
                        DesignDepartment_Id = c.Guid(nullable: false),
                        ProductBlockEngineer_Id = c.Guid(nullable: false),
                        ProductBlockManager_Id = c.Guid(nullable: false),
                        UserConstructor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.ParentPriceEngineeringTaskId)
                .ForeignKey("dbo.DesignDepartment", t => t.DesignDepartment_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlockEngineer_Id)
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlockManager_Id)
                .ForeignKey("dbo.User", t => t.UserConstructor_Id)
                .ForeignKey("dbo.PriceEngineeringTasks", t => t.ParentPriceEngineeringTasksId)
                .Index(t => t.ParentPriceEngineeringTasksId)
                .Index(t => t.ParentPriceEngineeringTaskId)
                .Index(t => t.DesignDepartment_Id)
                .Index(t => t.ProductBlockEngineer_Id)
                .Index(t => t.ProductBlockManager_Id)
                .Index(t => t.UserConstructor_Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskFileAnswer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreationMoment = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Comment = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTaskId)
                .Index(t => t.PriceEngineeringTaskId);
            
            CreateTable(
                "dbo.PriceEngineeringTaskFileTechnicalRequirements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreationMoment = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Comment = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskMessage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                        Message = c.String(nullable: false),
                        Author_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Author_Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTaskId)
                .Index(t => t.PriceEngineeringTaskId)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskProductBlockAdded",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(nullable: false),
                        Amount = c.Int(nullable: false),
                        IsOnBlock = c.Boolean(nullable: false),
                        ProductBlock_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlock_Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTaskId)
                .Index(t => t.PriceEngineeringTaskId)
                .Index(t => t.ProductBlock_Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 1024),
                        StatusEnum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTaskId)
                .Index(t => t.PriceEngineeringTaskId);
            
            CreateTable(
                "dbo.PriceEngineeringTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        WorkUpTo = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 1024),
                        UserManager_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserManager_Id)
                .Index(t => t.UserManager_Id);
            
            CreateTable(
                "dbo.PriceEngineeringTasksFileTechnicalRequirements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PrEngTasksId = c.Guid(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreationMoment = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Comment = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTasks", t => t.PrEngTasksId)
                .Index(t => t.PrEngTasksId);
            
            CreateTable(
                "dbo.DesignDepartmentParametersParameter",
                c => new
                    {
                        DesignDepartmentParameters_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DesignDepartmentParameters_Id, t.Parameter_Id })
                .ForeignKey("dbo.DesignDepartmentParameters", t => t.DesignDepartmentParameters_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.DesignDepartmentParameters_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.DesignDepartmentParametersAddedBlocksParameter",
                c => new
                    {
                        DesignDepartmentParametersAddedBlocks_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DesignDepartmentParametersAddedBlocks_Id, t.Parameter_Id })
                .ForeignKey("dbo.DesignDepartmentParametersAddedBlocks", t => t.DesignDepartmentParametersAddedBlocks_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.DesignDepartmentParametersAddedBlocks_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.DesignDepartmentUser",
                c => new
                    {
                        DesignDepartment_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DesignDepartment_Id, t.User_Id })
                .ForeignKey("dbo.DesignDepartment", t => t.DesignDepartment_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.DesignDepartment_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements",
                c => new
                    {
                        PriceEngineeringTask_Id = c.Guid(nullable: false),
                        PriceEngineeringTaskFileTechnicalRequirements_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PriceEngineeringTask_Id, t.PriceEngineeringTaskFileTechnicalRequirements_Id })
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTask_Id, cascadeDelete: true)
                .ForeignKey("dbo.PriceEngineeringTaskFileTechnicalRequirements", t => t.PriceEngineeringTaskFileTechnicalRequirements_Id, cascadeDelete: true)
                .Index(t => t.PriceEngineeringTask_Id)
                .Index(t => t.PriceEngineeringTaskFileTechnicalRequirements_Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskSalesUnit",
                c => new
                    {
                        PriceEngineeringTask_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PriceEngineeringTask_Id, t.SalesUnit_Id })
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTask_Id, cascadeDelete: true)
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .Index(t => t.PriceEngineeringTask_Id)
                .Index(t => t.SalesUnit_Id);
            
            AddColumn("dbo.PriceCalculation", "PriceEngineeringTasksId", c => c.Guid());
            AlterColumn("dbo.UserRole", "Name", c => c.String(nullable: false, maxLength: 64));
            CreateIndex("dbo.PriceCalculation", "PriceEngineeringTasksId");
            AddForeignKey("dbo.PriceCalculation", "PriceEngineeringTasksId", "dbo.PriceEngineeringTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTasks", "UserManager_Id", "dbo.User");
            DropForeignKey("dbo.PriceCalculation", "PriceEngineeringTasksId", "dbo.PriceEngineeringTasks");
            DropForeignKey("dbo.PriceEngineeringTasksFileTechnicalRequirements", "PrEngTasksId", "dbo.PriceEngineeringTasks");
            DropForeignKey("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId", "dbo.PriceEngineeringTasks");
            DropForeignKey("dbo.PriceEngineeringTask", "UserConstructor_Id", "dbo.User");
            DropForeignKey("dbo.PriceEngineeringTaskStatus", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskSalesUnit", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.PriceEngineeringTaskSalesUnit", "PriceEngineeringTask_Id", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskProductBlockAdded", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskProductBlockAdded", "ProductBlock_Id", "dbo.ProductBlock");
            DropForeignKey("dbo.PriceEngineeringTask", "ProductBlockManager_Id", "dbo.ProductBlock");
            DropForeignKey("dbo.PriceEngineeringTask", "ProductBlockEngineer_Id", "dbo.ProductBlock");
            DropForeignKey("dbo.PriceEngineeringTaskMessage", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskMessage", "Author_Id", "dbo.User");
            DropForeignKey("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTaskFileTechnicalRequirements_Id", "dbo.PriceEngineeringTaskFileTechnicalRequirements");
            DropForeignKey("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTask_Id", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskFileAnswer", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTask", "DesignDepartment_Id", "dbo.DesignDepartment");
            DropForeignKey("dbo.PriceEngineeringTask", "ParentPriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.DesignDepartmentUser", "User_Id", "dbo.User");
            DropForeignKey("dbo.DesignDepartmentUser", "DesignDepartment_Id", "dbo.DesignDepartment");
            DropForeignKey("dbo.DesignDepartmentParametersAddedBlocks", "DesignDepartmentId", "dbo.DesignDepartment");
            DropForeignKey("dbo.DesignDepartmentParametersAddedBlocksParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.DesignDepartmentParametersAddedBlocksParameter", "DesignDepartmentParametersAddedBlocks_Id", "dbo.DesignDepartmentParametersAddedBlocks");
            DropForeignKey("dbo.DesignDepartmentParameters", "DesignDepartmentId", "dbo.DesignDepartment");
            DropForeignKey("dbo.DesignDepartmentParametersParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.DesignDepartmentParametersParameter", "DesignDepartmentParameters_Id", "dbo.DesignDepartmentParameters");
            DropForeignKey("dbo.DesignDepartment", "Head_Id", "dbo.User");
            DropIndex("dbo.PriceEngineeringTaskSalesUnit", new[] { "SalesUnit_Id" });
            DropIndex("dbo.PriceEngineeringTaskSalesUnit", new[] { "PriceEngineeringTask_Id" });
            DropIndex("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements", new[] { "PriceEngineeringTaskFileTechnicalRequirements_Id" });
            DropIndex("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements", new[] { "PriceEngineeringTask_Id" });
            DropIndex("dbo.DesignDepartmentUser", new[] { "User_Id" });
            DropIndex("dbo.DesignDepartmentUser", new[] { "DesignDepartment_Id" });
            DropIndex("dbo.DesignDepartmentParametersAddedBlocksParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.DesignDepartmentParametersAddedBlocksParameter", new[] { "DesignDepartmentParametersAddedBlocks_Id" });
            DropIndex("dbo.DesignDepartmentParametersParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.DesignDepartmentParametersParameter", new[] { "DesignDepartmentParameters_Id" });
            DropIndex("dbo.PriceEngineeringTasksFileTechnicalRequirements", new[] { "PrEngTasksId" });
            DropIndex("dbo.PriceEngineeringTasks", new[] { "UserManager_Id" });
            DropIndex("dbo.PriceEngineeringTaskStatus", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTaskProductBlockAdded", new[] { "ProductBlock_Id" });
            DropIndex("dbo.PriceEngineeringTaskProductBlockAdded", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTaskMessage", new[] { "Author_Id" });
            DropIndex("dbo.PriceEngineeringTaskMessage", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTaskFileAnswer", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "UserConstructor_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ProductBlockManager_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ProductBlockEngineer_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "DesignDepartment_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ParentPriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ParentPriceEngineeringTasksId" });
            DropIndex("dbo.PriceCalculation", new[] { "PriceEngineeringTasksId" });
            DropIndex("dbo.DesignDepartmentParametersAddedBlocks", new[] { "DesignDepartmentId" });
            DropIndex("dbo.DesignDepartmentParameters", new[] { "DesignDepartmentId" });
            DropIndex("dbo.DesignDepartment", new[] { "Head_Id" });
            AlterColumn("dbo.UserRole", "Name", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.PriceCalculation", "PriceEngineeringTasksId");
            DropTable("dbo.PriceEngineeringTaskSalesUnit");
            DropTable("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements");
            DropTable("dbo.DesignDepartmentUser");
            DropTable("dbo.DesignDepartmentParametersAddedBlocksParameter");
            DropTable("dbo.DesignDepartmentParametersParameter");
            DropTable("dbo.PriceEngineeringTasksFileTechnicalRequirements");
            DropTable("dbo.PriceEngineeringTasks");
            DropTable("dbo.PriceEngineeringTaskStatus");
            DropTable("dbo.PriceEngineeringTaskProductBlockAdded");
            DropTable("dbo.PriceEngineeringTaskMessage");
            DropTable("dbo.PriceEngineeringTaskFileTechnicalRequirements");
            DropTable("dbo.PriceEngineeringTaskFileAnswer");
            DropTable("dbo.PriceEngineeringTask");
            DropTable("dbo.DesignDepartmentParametersAddedBlocks");
            DropTable("dbo.DesignDepartmentParameters");
            DropTable("dbo.DesignDepartment");
        }
    }
}
