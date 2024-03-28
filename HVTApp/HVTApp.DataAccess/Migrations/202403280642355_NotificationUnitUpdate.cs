namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationUnitUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotificationUnit", "Moment", c => c.DateTime(nullable: false));
            AddColumn("dbo.NotificationUnit", "IsSentByEmail", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotificationUnit", "IsSentByEmail");
            DropColumn("dbo.NotificationUnit", "Moment");
        }
    }
}
