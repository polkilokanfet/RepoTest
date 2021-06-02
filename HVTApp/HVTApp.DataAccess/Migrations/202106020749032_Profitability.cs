namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profitability : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CostsPercents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ManagmentCosts = c.Double(nullable: false),
                        EconomicCosts = c.Double(nullable: false),
                        CommercialCosts = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LaborHours",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LaborHoursParameter",
                c => new
                    {
                        LaborHours_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LaborHours_Id, t.Parameter_Id })
                .ForeignKey("dbo.LaborHours", t => t.LaborHours_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.LaborHours_Id)
                .Index(t => t.Parameter_Id);
            
            AddColumn("dbo.SalesUnit", "LaborHours", c => c.Double());
            AddColumn("dbo.SumOnDate", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LaborHoursParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.LaborHoursParameter", "LaborHours_Id", "dbo.LaborHours");
            DropIndex("dbo.LaborHoursParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.LaborHoursParameter", new[] { "LaborHours_Id" });
            DropColumn("dbo.SumOnDate", "Discriminator");
            DropColumn("dbo.SalesUnit", "LaborHours");
            DropTable("dbo.LaborHoursParameter");
            DropTable("dbo.LaborHours");
            DropTable("dbo.CostsPercents");
        }
    }
}
