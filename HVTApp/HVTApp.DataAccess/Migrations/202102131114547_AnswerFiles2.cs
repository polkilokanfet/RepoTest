namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnswerFiles2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerFileTce",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TechnicalRequrementsTaskId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Comment = c.String(maxLength: 250),
                        IsActual = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TechnicalRequrementsTask", t => t.TechnicalRequrementsTaskId)
                .Index(t => t.TechnicalRequrementsTaskId);
            
            AddColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesAnswersPath", c => c.String(maxLength: 500));
            AddColumn("dbo.PriceCalculation", "Initiator_Id", c => c.Guid());
            AddColumn("dbo.TechnicalRequrementsTask", "LogisticsCalculationRequired", c => c.Boolean(nullable: false));
            AlterColumn("dbo.GlobalProperties", "IncomingRequestsPath", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.GlobalProperties", "DirectumAttachmentsPath", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesPath", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.GlobalProperties", "PriceCalculationsFilesPath", c => c.String(nullable: false, maxLength: 500));
            CreateIndex("dbo.PriceCalculation", "Initiator_Id");
            AddForeignKey("dbo.PriceCalculation", "Initiator_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswerFileTce", "TechnicalRequrementsTaskId", "dbo.TechnicalRequrementsTask");
            DropForeignKey("dbo.PriceCalculation", "Initiator_Id", "dbo.User");
            DropIndex("dbo.PriceCalculation", new[] { "Initiator_Id" });
            DropIndex("dbo.AnswerFileTce", new[] { "TechnicalRequrementsTaskId" });
            AlterColumn("dbo.GlobalProperties", "PriceCalculationsFilesPath", c => c.String(nullable: false));
            AlterColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesPath", c => c.String(nullable: false));
            AlterColumn("dbo.GlobalProperties", "DirectumAttachmentsPath", c => c.String(nullable: false));
            AlterColumn("dbo.GlobalProperties", "IncomingRequestsPath", c => c.String(nullable: false));
            DropColumn("dbo.TechnicalRequrementsTask", "LogisticsCalculationRequired");
            DropColumn("dbo.PriceCalculation", "Initiator_Id");
            DropColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesAnswersPath");
            DropTable("dbo.AnswerFileTce");
        }
    }
}
