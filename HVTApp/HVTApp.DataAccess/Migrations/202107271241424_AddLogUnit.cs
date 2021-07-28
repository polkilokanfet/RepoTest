namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogUnit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogUnit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                        Head = c.String(nullable: false),
                        Message = c.String(),
                        Author_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogUnit", "Author_Id", "dbo.User");
            DropIndex("dbo.LogUnit", new[] { "Author_Id" });
            DropTable("dbo.LogUnit");
        }
    }
}
