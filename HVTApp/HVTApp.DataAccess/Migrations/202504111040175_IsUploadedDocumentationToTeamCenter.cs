namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsUploadedDocumentationToTeamCenter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "IsUploadedDocumentationToTeamCenter", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceEngineeringTask", "IsUploadedDocumentationToTeamCenter");
        }
    }
}
