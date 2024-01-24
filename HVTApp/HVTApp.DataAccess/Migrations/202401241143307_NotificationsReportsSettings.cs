namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationsReportsSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationsReportsSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChiefEngineerReportMoment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotificationsReportsSettingsUser",
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
            DropForeignKey("dbo.NotificationsReportsSettingsUser", "User_Id", "dbo.User");
            DropForeignKey("dbo.NotificationsReportsSettingsUser", "NotificationsReportsSettings_Id", "dbo.NotificationsReportsSettings");
            DropIndex("dbo.NotificationsReportsSettingsUser", new[] { "User_Id" });
            DropIndex("dbo.NotificationsReportsSettingsUser", new[] { "NotificationsReportsSettings_Id" });
            DropTable("dbo.NotificationsReportsSettingsUser");
            DropTable("dbo.NotificationsReportsSettings");
        }
    }
}
