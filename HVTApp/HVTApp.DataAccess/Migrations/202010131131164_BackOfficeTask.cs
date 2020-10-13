namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackOfficeTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TechnicalRequrements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Comment = c.String(maxLength: 250),
                        TechnicalRequrementsTask_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TechnicalRequrementsTask", t => t.TechnicalRequrementsTask_Id)
                .Index(t => t.TechnicalRequrementsTask_Id);
            
            CreateTable(
                "dbo.TechnicalRequrementsFile",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Comment = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TechnicalRequrementsTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Comment = c.String(maxLength: 250),
                        TceNumber = c.String(maxLength: 10),
                        Start = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TechnicalRequrementsTechnicalRequrementsFile",
                c => new
                    {
                        TechnicalRequrements_Id = c.Guid(nullable: false),
                        TechnicalRequrementsFile_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.TechnicalRequrements_Id, t.TechnicalRequrementsFile_Id })
                .ForeignKey("dbo.TechnicalRequrements", t => t.TechnicalRequrements_Id, cascadeDelete: true)
                .ForeignKey("dbo.TechnicalRequrementsFile", t => t.TechnicalRequrementsFile_Id, cascadeDelete: true)
                .Index(t => t.TechnicalRequrements_Id)
                .Index(t => t.TechnicalRequrementsFile_Id);
            
            CreateTable(
                "dbo.TechnicalRequrementsSalesUnit",
                c => new
                    {
                        TechnicalRequrements_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.TechnicalRequrements_Id, t.SalesUnit_Id })
                .ForeignKey("dbo.TechnicalRequrements", t => t.TechnicalRequrements_Id, cascadeDelete: true)
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .Index(t => t.TechnicalRequrements_Id)
                .Index(t => t.SalesUnit_Id);
            
            AddColumn("dbo.User", "TechnicalRequrementsTask_Id", c => c.Guid());
            AddColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesPath", c => c.String());
            AlterColumn("dbo.GlobalProperties", "IncomingRequestsPath", c => c.String(nullable: false));
            AlterColumn("dbo.GlobalProperties", "DirectumAttachmentsPath", c => c.String(nullable: false));
            CreateIndex("dbo.User", "TechnicalRequrementsTask_Id");
            AddForeignKey("dbo.User", "TechnicalRequrementsTask_Id", "dbo.TechnicalRequrementsTask", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TechnicalRequrements", "TechnicalRequrementsTask_Id", "dbo.TechnicalRequrementsTask");
            DropForeignKey("dbo.User", "TechnicalRequrementsTask_Id", "dbo.TechnicalRequrementsTask");
            DropForeignKey("dbo.TechnicalRequrementsSalesUnit", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.TechnicalRequrementsSalesUnit", "TechnicalRequrements_Id", "dbo.TechnicalRequrements");
            DropForeignKey("dbo.TechnicalRequrementsTechnicalRequrementsFile", "TechnicalRequrementsFile_Id", "dbo.TechnicalRequrementsFile");
            DropForeignKey("dbo.TechnicalRequrementsTechnicalRequrementsFile", "TechnicalRequrements_Id", "dbo.TechnicalRequrements");
            DropIndex("dbo.TechnicalRequrementsSalesUnit", new[] { "SalesUnit_Id" });
            DropIndex("dbo.TechnicalRequrementsSalesUnit", new[] { "TechnicalRequrements_Id" });
            DropIndex("dbo.TechnicalRequrementsTechnicalRequrementsFile", new[] { "TechnicalRequrementsFile_Id" });
            DropIndex("dbo.TechnicalRequrementsTechnicalRequrementsFile", new[] { "TechnicalRequrements_Id" });
            DropIndex("dbo.TechnicalRequrements", new[] { "TechnicalRequrementsTask_Id" });
            DropIndex("dbo.User", new[] { "TechnicalRequrementsTask_Id" });
            AlterColumn("dbo.GlobalProperties", "DirectumAttachmentsPath", c => c.String());
            AlterColumn("dbo.GlobalProperties", "IncomingRequestsPath", c => c.String());
            DropColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesPath");
            DropColumn("dbo.User", "TechnicalRequrementsTask_Id");
            DropTable("dbo.TechnicalRequrementsSalesUnit");
            DropTable("dbo.TechnicalRequrementsTechnicalRequrementsFile");
            DropTable("dbo.TechnicalRequrementsTask");
            DropTable("dbo.TechnicalRequrementsFile");
            DropTable("dbo.TechnicalRequrements");
        }
    }
}
