namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceEngineeringTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Int(nullable: false),
                        ParentPriceEngineeringTaskId = c.Guid(nullable: false),
                        ProductBlockEngineer_Id = c.Guid(nullable: false),
                        ProductBlockManager_Id = c.Guid(nullable: false),
                        UserConstructor_Id = c.Guid(),
                        UserManager_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.ParentPriceEngineeringTaskId)
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlockEngineer_Id)
                .ForeignKey("dbo.ProductBlock", t => t.ProductBlockManager_Id)
                .ForeignKey("dbo.User", t => t.UserConstructor_Id)
                .ForeignKey("dbo.User", t => t.UserManager_Id)
                .Index(t => t.ParentPriceEngineeringTaskId)
                .Index(t => t.ProductBlockEngineer_Id)
                .Index(t => t.ProductBlockManager_Id)
                .Index(t => t.UserConstructor_Id)
                .Index(t => t.UserManager_Id);
            
            CreateTable(
                "dbo.PriceEngineeringTaskFileAnswer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreationMoment = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Comment = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTaskId)
                .Index(t => t.PriceEngineeringTaskId);
            
            CreateTable(
                "dbo.PriceEngineeringTaskFileTechnicalRequirements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceEngineeringTaskId = c.Guid(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CoversChildTasks = c.Boolean(nullable: false),
                        CreationMoment = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Comment = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTaskId)
                .Index(t => t.PriceEngineeringTaskId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTask", "UserManager_Id", "dbo.User");
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
            DropForeignKey("dbo.PriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTaskFileAnswer", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.PriceEngineeringTask", "ParentPriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropIndex("dbo.PriceEngineeringTaskSalesUnit", new[] { "SalesUnit_Id" });
            DropIndex("dbo.PriceEngineeringTaskSalesUnit", new[] { "PriceEngineeringTask_Id" });
            DropIndex("dbo.PriceEngineeringTaskStatus", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTaskProductBlockAdded", new[] { "ProductBlock_Id" });
            DropIndex("dbo.PriceEngineeringTaskProductBlockAdded", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTaskMessage", new[] { "Author_Id" });
            DropIndex("dbo.PriceEngineeringTaskMessage", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTaskFileTechnicalRequirements", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTaskFileAnswer", new[] { "PriceEngineeringTaskId" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "UserManager_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "UserConstructor_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ProductBlockManager_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ProductBlockEngineer_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ParentPriceEngineeringTaskId" });
            DropTable("dbo.PriceEngineeringTaskSalesUnit");
            DropTable("dbo.PriceEngineeringTaskStatus");
            DropTable("dbo.PriceEngineeringTaskProductBlockAdded");
            DropTable("dbo.PriceEngineeringTaskMessage");
            DropTable("dbo.PriceEngineeringTaskFileTechnicalRequirements");
            DropTable("dbo.PriceEngineeringTaskFileAnswer");
            DropTable("dbo.PriceEngineeringTask");
        }
    }
}
