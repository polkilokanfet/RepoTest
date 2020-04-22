namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Complects : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Comment", c => c.String(maxLength: 256));
            AddColumn("dbo.GlobalProperties", "ComplectDesignationGroup_Id", c => c.Guid());
            AddColumn("dbo.GlobalProperties", "ComplectsGroup_Id", c => c.Guid());
            AddColumn("dbo.GlobalProperties", "ComplectsParameter_Id", c => c.Guid());
            CreateIndex("dbo.GlobalProperties", "ComplectDesignationGroup_Id");
            CreateIndex("dbo.GlobalProperties", "ComplectsGroup_Id");
            CreateIndex("dbo.GlobalProperties", "ComplectsParameter_Id");
            AddForeignKey("dbo.GlobalProperties", "ComplectDesignationGroup_Id", "dbo.ParameterGroup", "Id");
            AddForeignKey("dbo.GlobalProperties", "ComplectsGroup_Id", "dbo.ParameterGroup", "Id");
            AddForeignKey("dbo.GlobalProperties", "ComplectsParameter_Id", "dbo.Parameter", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlobalProperties", "ComplectsParameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.GlobalProperties", "ComplectsGroup_Id", "dbo.ParameterGroup");
            DropForeignKey("dbo.GlobalProperties", "ComplectDesignationGroup_Id", "dbo.ParameterGroup");
            DropIndex("dbo.GlobalProperties", new[] { "ComplectsParameter_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "ComplectsGroup_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "ComplectDesignationGroup_Id" });
            DropColumn("dbo.GlobalProperties", "ComplectsParameter_Id");
            DropColumn("dbo.GlobalProperties", "ComplectsGroup_Id");
            DropColumn("dbo.GlobalProperties", "ComplectDesignationGroup_Id");
            DropColumn("dbo.Product", "Comment");
        }
    }
}
