namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryTCE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShippingCostFile",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TechnicalRequrementsTaskId = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TechnicalRequrementsTask", t => t.TechnicalRequrementsTaskId)
                .Index(t => t.TechnicalRequrementsTaskId);
            
            CreateTable(
                "dbo.TechnicalRequrementsTaskHistoryElement",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TechnicalRequrementsTaskId = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Comment = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TechnicalRequrementsTask", t => t.TechnicalRequrementsTaskId)
                .Index(t => t.TechnicalRequrementsTaskId);
            
            AddColumn("dbo.AnswerFileTce", "Date", c => c.DateTime());
            AddColumn("dbo.GlobalProperties", "ShippingCostFilesPath", c => c.String(maxLength: 500));
            AddColumn("dbo.TechnicalRequrementsFile", "Date", c => c.DateTime());
            DropColumn("dbo.TechnicalRequrementsTask", "Finish");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TechnicalRequrementsTask", "Finish", c => c.DateTime());
            DropForeignKey("dbo.ShippingCostFile", "TechnicalRequrementsTaskId", "dbo.TechnicalRequrementsTask");
            DropForeignKey("dbo.TechnicalRequrementsTaskHistoryElement", "TechnicalRequrementsTaskId", "dbo.TechnicalRequrementsTask");
            DropIndex("dbo.TechnicalRequrementsTaskHistoryElement", new[] { "TechnicalRequrementsTaskId" });
            DropIndex("dbo.ShippingCostFile", new[] { "TechnicalRequrementsTaskId" });
            DropColumn("dbo.TechnicalRequrementsFile", "Date");
            DropColumn("dbo.GlobalProperties", "ShippingCostFilesPath");
            DropColumn("dbo.AnswerFileTce", "Date");
            DropTable("dbo.TechnicalRequrementsTaskHistoryElement");
            DropTable("dbo.ShippingCostFile");
        }
    }
}
