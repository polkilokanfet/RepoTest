namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParameterGroupPowerful : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParameterGroup", "Powerful", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParameterGroup", "Powerful");
        }
    }
}
