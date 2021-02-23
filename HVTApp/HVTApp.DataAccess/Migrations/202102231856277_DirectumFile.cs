namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DirectumFile : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PriceCalculation", new[] { "Initiator_Id" });
            CreateTable(
                "dbo.DirectumTaskGroupFile",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        LoadMoment = c.DateTime(nullable: false),
                        DirectumTaskGroupId = c.Guid(nullable: false),
                        Author_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Author_Id)
                .ForeignKey("dbo.DirectumTaskGroup", t => t.DirectumTaskGroupId)
                .Index(t => t.DirectumTaskGroupId)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroupUser",
                c => new
                    {
                        UserGroup_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroup_Id, t.User_Id })
                .ForeignKey("dbo.UserGroup", t => t.UserGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.UserGroup_Id)
                .Index(t => t.User_Id);
            
            AlterColumn("dbo.DirectumTaskGroup", "Message", c => c.String(nullable: false));
            AlterColumn("dbo.DirectumTaskMessage", "Message", c => c.String(nullable: false));
            AlterColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesAnswersPath", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.PriceCalculation", "Initiator_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PriceCalculation", "Initiator_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroupUser", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserGroupUser", "UserGroup_Id", "dbo.UserGroup");
            DropForeignKey("dbo.DirectumTaskGroupFile", "DirectumTaskGroupId", "dbo.DirectumTaskGroup");
            DropForeignKey("dbo.DirectumTaskGroupFile", "Author_Id", "dbo.User");
            DropIndex("dbo.UserGroupUser", new[] { "User_Id" });
            DropIndex("dbo.UserGroupUser", new[] { "UserGroup_Id" });
            DropIndex("dbo.PriceCalculation", new[] { "Initiator_Id" });
            DropIndex("dbo.DirectumTaskGroupFile", new[] { "Author_Id" });
            DropIndex("dbo.DirectumTaskGroupFile", new[] { "DirectumTaskGroupId" });
            AlterColumn("dbo.PriceCalculation", "Initiator_Id", c => c.Guid());
            AlterColumn("dbo.GlobalProperties", "TechnicalRequrementsFilesAnswersPath", c => c.String(maxLength: 500));
            AlterColumn("dbo.DirectumTaskMessage", "Message", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.DirectumTaskGroup", "Message", c => c.String(nullable: false, maxLength: 1000));
            DropTable("dbo.UserGroupUser");
            DropTable("dbo.UserGroup");
            DropTable("dbo.DirectumTaskGroupFile");
            CreateIndex("dbo.PriceCalculation", "Initiator_Id");
        }
    }
}
