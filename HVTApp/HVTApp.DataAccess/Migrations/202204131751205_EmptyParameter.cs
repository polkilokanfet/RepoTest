namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyParameter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlobalProperties", "EmptyParameterCurrentTransformersSet_Id", c => c.Guid());
            AddColumn("dbo.GlobalProperties", "ParameterCurrentTransformersSetCustom_Id", c => c.Guid());
            CreateIndex("dbo.GlobalProperties", "EmptyParameterCurrentTransformersSet_Id");
            CreateIndex("dbo.GlobalProperties", "ParameterCurrentTransformersSetCustom_Id");
            AddForeignKey("dbo.GlobalProperties", "EmptyParameterCurrentTransformersSet_Id", "dbo.Parameter", "Id");
            AddForeignKey("dbo.GlobalProperties", "ParameterCurrentTransformersSetCustom_Id", "dbo.Parameter", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlobalProperties", "ParameterCurrentTransformersSetCustom_Id", "dbo.Parameter");
            DropForeignKey("dbo.GlobalProperties", "EmptyParameterCurrentTransformersSet_Id", "dbo.Parameter");
            DropIndex("dbo.GlobalProperties", new[] { "ParameterCurrentTransformersSetCustom_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "EmptyParameterCurrentTransformersSet_Id" });
            DropColumn("dbo.GlobalProperties", "ParameterCurrentTransformersSetCustom_Id");
            DropColumn("dbo.GlobalProperties", "EmptyParameterCurrentTransformersSet_Id");
        }
    }
}
