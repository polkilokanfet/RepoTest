namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeqReqChanges4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TechnicalRequrements", "IsActual", c => c.Boolean());
            AddColumn("dbo.TechnicalRequrementsFile", "IsActual", c => c.Boolean());
            AddColumn("dbo.TechnicalRequrementsTask", "Finish", c => c.DateTime());
            AddColumn("dbo.TechnicalRequrementsTask", "LastOpenBackManagerMoment", c => c.DateTime());
            AddColumn("dbo.TechnicalRequrementsTask", "FirstStartMoment", c => c.DateTime());
            AddColumn("dbo.TechnicalRequrementsTask", "RejectByBackManagerMoment", c => c.DateTime());
            AddColumn("dbo.TechnicalRequrementsTask", "RejectComment", c => c.String(maxLength: 250));
            AlterColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesPath", c => c.String(nullable: false));
            AlterColumn("dbo.GlobalProperties", "PriceCalculationsFilesPath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GlobalProperties", "PriceCalculationsFilesPath", c => c.String());
            AlterColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesPath", c => c.String());
            DropColumn("dbo.TechnicalRequrementsTask", "RejectComment");
            DropColumn("dbo.TechnicalRequrementsTask", "RejectByBackManagerMoment");
            DropColumn("dbo.TechnicalRequrementsTask", "FirstStartMoment");
            DropColumn("dbo.TechnicalRequrementsTask", "LastOpenBackManagerMoment");
            DropColumn("dbo.TechnicalRequrementsTask", "Finish");
            DropColumn("dbo.TechnicalRequrementsFile", "IsActual");
            DropColumn("dbo.TechnicalRequrements", "IsActual");
        }
    }
}
