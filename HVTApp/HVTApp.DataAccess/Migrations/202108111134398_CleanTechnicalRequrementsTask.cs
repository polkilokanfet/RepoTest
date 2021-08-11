namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanTechnicalRequrementsTask : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnswerFileTce", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.GlobalProperties", "ShippingCostFilesPath", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.TechnicalRequrementsFile", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.TechnicalRequrementsTask", "Comment");
            DropColumn("dbo.TechnicalRequrementsTask", "CommentBackOfficeBoss");
            DropColumn("dbo.TechnicalRequrementsTask", "Start");
            DropColumn("dbo.TechnicalRequrementsTask", "FirstStartMoment");
            DropColumn("dbo.TechnicalRequrementsTask", "RejectByBackManagerMoment");
            DropColumn("dbo.TechnicalRequrementsTask", "RejectComment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TechnicalRequrementsTask", "RejectComment", c => c.String(maxLength: 250));
            AddColumn("dbo.TechnicalRequrementsTask", "RejectByBackManagerMoment", c => c.DateTime());
            AddColumn("dbo.TechnicalRequrementsTask", "FirstStartMoment", c => c.DateTime());
            AddColumn("dbo.TechnicalRequrementsTask", "Start", c => c.DateTime());
            AddColumn("dbo.TechnicalRequrementsTask", "CommentBackOfficeBoss", c => c.String(maxLength: 250));
            AddColumn("dbo.TechnicalRequrementsTask", "Comment", c => c.String(maxLength: 250));
            AlterColumn("dbo.TechnicalRequrementsFile", "Date", c => c.DateTime());
            AlterColumn("dbo.GlobalProperties", "ShippingCostFilesPath", c => c.String(maxLength: 500));
            AlterColumn("dbo.AnswerFileTce", "Date", c => c.DateTime());
        }
    }
}
