namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTaskStatusCommentLenghRemove : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PriceEngineeringTaskStatus", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PriceEngineeringTaskStatus", "Comment", c => c.String(maxLength: 1024));
        }
    }
}
