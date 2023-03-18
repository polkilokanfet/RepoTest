namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventServiceUnitUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventServiceUnit", "Role", c => c.Int());
            AddColumn("dbo.EventServiceUnit", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventServiceUnit", "Message");
            DropColumn("dbo.EventServiceUnit", "Role");
        }
    }
}
