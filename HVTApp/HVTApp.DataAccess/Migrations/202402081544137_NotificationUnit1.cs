namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationUnit1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationUnit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ActionType = c.Int(nullable: false),
                        TargetEntityId = c.Guid(nullable: false),
                        SenderUserId = c.Guid(nullable: false),
                        SenderRole = c.Int(nullable: false),
                        RecipientUserId = c.Guid(nullable: false),
                        RecipientRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.RecipientUserId)
                .ForeignKey("dbo.User", t => t.SenderUserId)
                .Index(t => t.SenderUserId)
                .Index(t => t.RecipientUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationUnit", "SenderUserId", "dbo.User");
            DropForeignKey("dbo.NotificationUnit", "RecipientUserId", "dbo.User");
            DropIndex("dbo.NotificationUnit", new[] { "RecipientUserId" });
            DropIndex("dbo.NotificationUnit", new[] { "SenderUserId" });
            DropTable("dbo.NotificationUnit");
        }
    }
}
