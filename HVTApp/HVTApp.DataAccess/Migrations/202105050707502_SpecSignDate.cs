namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecSignDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specification", "SignDate", c => c.DateTime());
            AddColumn("dbo.GlobalProperties", "LogsPath", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GlobalProperties", "LogsPath");
            DropColumn("dbo.Specification", "SignDate");
        }
    }
}
