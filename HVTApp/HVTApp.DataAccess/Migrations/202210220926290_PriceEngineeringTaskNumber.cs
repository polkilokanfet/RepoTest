namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEngineeringTaskNumber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceEngineeringTaskNumber",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: true),
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Number);
            
            CreateTable(
                "dbo.PriceEngineeringTasksNumber",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: true),
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Number);
            
            AddColumn("dbo.PriceEngineeringTask", "Number_Number", c => c.Int());
            AddColumn("dbo.PriceEngineeringTasks", "Number_Number", c => c.Int());
            CreateIndex("dbo.PriceEngineeringTask", "Number_Number");
            CreateIndex("dbo.PriceEngineeringTasks", "Number_Number");
            AddForeignKey("dbo.PriceEngineeringTask", "Number_Number", "dbo.PriceEngineeringTaskNumber", "Number");
            AddForeignKey("dbo.PriceEngineeringTasks", "Number_Number", "dbo.PriceEngineeringTasksNumber", "Number");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEngineeringTasks", "Number_Number", "dbo.PriceEngineeringTasksNumber");
            DropForeignKey("dbo.PriceEngineeringTask", "Number_Number", "dbo.PriceEngineeringTaskNumber");
            DropIndex("dbo.PriceEngineeringTasks", new[] { "Number_Number" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "Number_Number" });
            DropColumn("dbo.PriceEngineeringTasks", "Number_Number");
            DropColumn("dbo.PriceEngineeringTask", "Number_Number");
            DropTable("dbo.PriceEngineeringTasksNumber");
            DropTable("dbo.PriceEngineeringTaskNumber");
        }
    }
}
