namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventServiceUnit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventServiceUnit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TargetEntityId = c.Guid(nullable: false),
                        EventServiceActionType = c.Int(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventServiceUnit", "User_Id", "dbo.User");
            DropIndex("dbo.EventServiceUnit", new[] { "User_Id" });
            DropTable("dbo.EventServiceUnit");
        }
    }
}
