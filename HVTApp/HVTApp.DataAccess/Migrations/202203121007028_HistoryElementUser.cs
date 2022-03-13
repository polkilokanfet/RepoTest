namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryElementUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TechnicalRequrementsTaskHistoryElement", "User_Id", c => c.Guid());
            CreateIndex("dbo.TechnicalRequrementsTaskHistoryElement", "User_Id");
            AddForeignKey("dbo.TechnicalRequrementsTaskHistoryElement", "User_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TechnicalRequrementsTaskHistoryElement", "User_Id", "dbo.User");
            DropIndex("dbo.TechnicalRequrementsTaskHistoryElement", new[] { "User_Id" });
            DropColumn("dbo.TechnicalRequrementsTaskHistoryElement", "User_Id");
        }
    }
}
