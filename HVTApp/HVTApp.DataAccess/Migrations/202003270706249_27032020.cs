namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27032020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncomingRequest", "InstructionDate", c => c.DateTime());
            AddColumn("dbo.IncomingRequest", "DoneDate", c => c.DateTime());
            DropColumn("dbo.IncomingRequest", "IsDone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IncomingRequest", "IsDone", c => c.Boolean(nullable: false));
            DropColumn("dbo.IncomingRequest", "DoneDate");
            DropColumn("dbo.IncomingRequest", "InstructionDate");
        }
    }
}
