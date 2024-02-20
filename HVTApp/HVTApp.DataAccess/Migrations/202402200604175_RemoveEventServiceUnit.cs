namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEventServiceUnit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventServiceUnit", "User_Id", "dbo.User");
            DropIndex("dbo.EventServiceUnit", new[] { "User_Id" });
            DropTable("dbo.EventServiceUnit");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventServiceUnit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Role = c.Int(),
                        TargetEntityId = c.Guid(nullable: false),
                        EventServiceActionType = c.Int(nullable: false),
                        Message = c.String(),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.EventServiceUnit", "User_Id");
            AddForeignKey("dbo.EventServiceUnit", "User_Id", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
