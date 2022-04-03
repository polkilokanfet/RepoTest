namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileTechnicalRequirementsEdit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask");
            DropIndex("dbo.PriceEngineeringTaskFileTechnicalRequirements", new[] { "PriceEngineeringTaskId" });
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
            
            DropColumn("dbo.PriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTaskId");
            DropColumn("dbo.PriceEngineeringTaskFileTechnicalRequirements", "CoversChildTasks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceEngineeringTaskFileTechnicalRequirements", "CoversChildTasks", c => c.Boolean(nullable: false));
            AddColumn("dbo.PriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTaskId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTaskFileTechnicalRequirements_Id", "dbo.PriceEngineeringTaskFileTechnicalRequirements");
            DropForeignKey("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTask_Id", "dbo.PriceEngineeringTask");
            DropIndex("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements", new[] { "PriceEngineeringTaskFileTechnicalRequirements_Id" });
            DropIndex("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements", new[] { "PriceEngineeringTask_Id" });
            DropTable("dbo.PriceEngineeringTaskPriceEngineeringTaskFileTechnicalRequirements");
            CreateIndex("dbo.PriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTaskId");
            AddForeignKey("dbo.PriceEngineeringTaskFileTechnicalRequirements", "PriceEngineeringTaskId", "dbo.PriceEngineeringTask", "Id");
        }
    }
}
