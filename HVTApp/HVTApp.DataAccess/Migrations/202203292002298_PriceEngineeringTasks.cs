namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTasks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceEngineeringTask", "UserManager_Id", "dbo.User");
            DropIndex("dbo.PriceEngineeringTask", new[] { "UserManager_Id" });
            CreateTable(
                "dbo.PriceEngineeringTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
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
                        PriceEngineeringTasksId = c.Guid(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        CreationMoment = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Comment = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceEngineeringTasks", t => t.PriceEngineeringTasksId)
                .Index(t => t.PriceEngineeringTasksId);
            
            AddColumn("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId");
            AddForeignKey("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId", "dbo.PriceEngineeringTasks", "Id");
            DropColumn("dbo.PriceEngineeringTask", "UserManager_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceEngineeringTask", "UserManager_Id", c => c.Guid(nullable: false));
            DropForeignKey("dbo.PriceEngineeringTasks", "UserManager_Id", "dbo.User");
            DropForeignKey("dbo.PriceEngineeringTasksFileTechnicalRequirements", "PriceEngineeringTasksId", "dbo.PriceEngineeringTasks");
            DropForeignKey("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId", "dbo.PriceEngineeringTasks");
            DropIndex("dbo.PriceEngineeringTasksFileTechnicalRequirements", new[] { "PriceEngineeringTasksId" });
            DropIndex("dbo.PriceEngineeringTasks", new[] { "UserManager_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "ParentPriceEngineeringTasksId" });
            DropColumn("dbo.PriceEngineeringTask", "ParentPriceEngineeringTasksId");
            DropTable("dbo.PriceEngineeringTasksFileTechnicalRequirements");
            DropTable("dbo.PriceEngineeringTasks");
            CreateIndex("dbo.PriceEngineeringTask", "UserManager_Id");
            AddForeignKey("dbo.PriceEngineeringTask", "UserManager_Id", "dbo.User", "Id");
        }
    }
}
