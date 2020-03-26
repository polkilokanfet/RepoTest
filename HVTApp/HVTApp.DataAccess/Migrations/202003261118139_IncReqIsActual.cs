namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncReqIsActual : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "TceNumber", c => c.String(maxLength: 20));
            AddColumn("dbo.IncomingRequest", "IsActual", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Document", "Comment", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Document", "Comment", c => c.String(maxLength: 100));
            DropColumn("dbo.IncomingRequest", "IsActual");
            DropColumn("dbo.Document", "TceNumber");
        }
    }
}
