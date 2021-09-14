namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TCETaskDesiredFinishDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TechnicalRequrementsTask", "DesiredFinishDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TechnicalRequrementsTask", "DesiredFinishDate");
        }
    }
}
