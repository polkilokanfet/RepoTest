namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryElementsTCE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TechnicalRequrementsTaskHistoryElement",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Comment = c.String(maxLength: 250),
                        TechnicalRequrementsTask_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TechnicalRequrementsTask", t => t.TechnicalRequrementsTask_Id)
                .Index(t => t.TechnicalRequrementsTask_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TechnicalRequrementsTaskHistoryElement", "TechnicalRequrementsTask_Id", "dbo.TechnicalRequrementsTask");
            DropIndex("dbo.TechnicalRequrementsTaskHistoryElement", new[] { "TechnicalRequrementsTask_Id" });
            DropTable("dbo.TechnicalRequrementsTaskHistoryElement");
        }
    }
}
