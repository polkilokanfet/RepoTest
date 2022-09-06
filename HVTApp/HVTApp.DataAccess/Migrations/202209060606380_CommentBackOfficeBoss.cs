namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentBackOfficeBoss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTasks", "CommentBackOfficeBoss", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceEngineeringTasks", "CommentBackOfficeBoss");
        }
    }
}
