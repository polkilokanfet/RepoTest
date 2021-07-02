namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfitAbilityComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LaborHours", "Comment", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LaborHours", "Comment");
        }
    }
}
