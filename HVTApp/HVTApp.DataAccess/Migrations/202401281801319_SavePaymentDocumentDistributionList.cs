namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SavePaymentDocumentDistributionList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationsReportsSettingsUser1",
                c => new
                    {
                        NotificationsReportsSettings_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.NotificationsReportsSettings_Id, t.User_Id })
                .ForeignKey("dbo.NotificationsReportsSettings", t => t.NotificationsReportsSettings_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.NotificationsReportsSettings_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationsReportsSettingsUser1", "User_Id", "dbo.User");
            DropForeignKey("dbo.NotificationsReportsSettingsUser1", "NotificationsReportsSettings_Id", "dbo.NotificationsReportsSettings");
            DropIndex("dbo.NotificationsReportsSettingsUser1", new[] { "User_Id" });
            DropIndex("dbo.NotificationsReportsSettingsUser1", new[] { "NotificationsReportsSettings_Id" });
            DropTable("dbo.NotificationsReportsSettingsUser1");
        }
    }
}
