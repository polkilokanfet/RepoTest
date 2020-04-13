namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Directum5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DirectumTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartPerformer = c.DateTime(),
                        FinishPlan = c.DateTime(nullable: false),
                        FinishPerformer = c.DateTime(),
                        FinishAuthor = c.DateTime(),
                        Group_Id = c.Guid(nullable: false),
                        ParentTask_Id = c.Guid(),
                        Performer_Id = c.Guid(nullable: false),
                        PreviousTask_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DirectumTaskGroup", t => t.Group_Id)
                .ForeignKey("dbo.DirectumTask", t => t.ParentTask_Id)
                .ForeignKey("dbo.User", t => t.Performer_Id)
                .ForeignKey("dbo.DirectumTask", t => t.PreviousTask_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.ParentTask_Id)
                .Index(t => t.Performer_Id)
                .Index(t => t.PreviousTask_Id);
            
            CreateTable(
                "dbo.DirectumTaskGroup",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 250),
                        StartAuthor = c.DateTime(nullable: false),
                        IsStoped = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        AttachmentsPath = c.String(maxLength: 1000),
                        Message = c.String(),
                        Author_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.DirectumTaskMessage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                        Message = c.String(nullable: false, maxLength: 1000),
                        Author_Id = c.Guid(nullable: false),
                        DirectumTask_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Author_Id)
                .ForeignKey("dbo.DirectumTask", t => t.DirectumTask_Id, cascadeDelete: true)
                .Index(t => t.Author_Id)
                .Index(t => t.DirectumTask_Id);
            
            CreateTable(
                "dbo.DirectumTaskGroupUser",
                c => new
                    {
                        DirectumTaskGroup_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DirectumTaskGroup_Id, t.User_Id })
                .ForeignKey("dbo.DirectumTaskGroup", t => t.DirectumTaskGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.DirectumTaskGroup_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DirectumTask", "PreviousTask_Id", "dbo.DirectumTask");
            DropForeignKey("dbo.DirectumTask", "Performer_Id", "dbo.User");
            DropForeignKey("dbo.DirectumTask", "ParentTask_Id", "dbo.DirectumTask");
            DropForeignKey("dbo.DirectumTaskMessage", "DirectumTask_Id", "dbo.DirectumTask");
            DropForeignKey("dbo.DirectumTaskMessage", "Author_Id", "dbo.User");
            DropForeignKey("dbo.DirectumTask", "Group_Id", "dbo.DirectumTaskGroup");
            DropForeignKey("dbo.DirectumTaskGroupUser", "User_Id", "dbo.User");
            DropForeignKey("dbo.DirectumTaskGroupUser", "DirectumTaskGroup_Id", "dbo.DirectumTaskGroup");
            DropForeignKey("dbo.DirectumTaskGroup", "Author_Id", "dbo.User");
            DropIndex("dbo.DirectumTaskGroupUser", new[] { "User_Id" });
            DropIndex("dbo.DirectumTaskGroupUser", new[] { "DirectumTaskGroup_Id" });
            DropIndex("dbo.DirectumTaskMessage", new[] { "DirectumTask_Id" });
            DropIndex("dbo.DirectumTaskMessage", new[] { "Author_Id" });
            DropIndex("dbo.DirectumTaskGroup", new[] { "Author_Id" });
            DropIndex("dbo.DirectumTask", new[] { "PreviousTask_Id" });
            DropIndex("dbo.DirectumTask", new[] { "Performer_Id" });
            DropIndex("dbo.DirectumTask", new[] { "ParentTask_Id" });
            DropIndex("dbo.DirectumTask", new[] { "Group_Id" });
            DropTable("dbo.DirectumTaskGroupUser");
            DropTable("dbo.DirectumTaskMessage");
            DropTable("dbo.DirectumTaskGroup");
            DropTable("dbo.DirectumTask");
        }
    }
}
