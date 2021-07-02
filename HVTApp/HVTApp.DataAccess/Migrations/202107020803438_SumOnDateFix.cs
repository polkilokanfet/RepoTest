namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SumOnDateFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LaborHourCost",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Sum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.SumOnDate", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SumOnDate", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.LaborHourCost");
        }
    }
}
