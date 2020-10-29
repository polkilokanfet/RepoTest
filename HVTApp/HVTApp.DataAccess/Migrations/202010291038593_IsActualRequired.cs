namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActualRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TechnicalRequrements", "IsActual", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TechnicalRequrementsFile", "IsActual", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TechnicalRequrementsFile", "IsActual", c => c.Boolean());
            AlterColumn("dbo.TechnicalRequrements", "IsActual", c => c.Boolean());
        }
    }
}
