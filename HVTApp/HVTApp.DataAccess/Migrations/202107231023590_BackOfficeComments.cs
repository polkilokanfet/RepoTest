namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackOfficeComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TechnicalRequrementsTask", "CommentBackOfficeBoss", c => c.String(maxLength: 250));
            AddColumn("dbo.TechnicalRequrementsTask", "ExcelFileIsRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TechnicalRequrementsTask", "ExcelFileIsRequired");
            DropColumn("dbo.TechnicalRequrementsTask", "CommentBackOfficeBoss");
        }
    }
}
