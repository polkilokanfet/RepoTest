namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TenderDidNotTakePlace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tender", "DidNotTakePlace", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tender", "DidNotTakePlace");
        }
    }
}
