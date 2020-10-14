namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceCalculationFile",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreationMoment = c.DateTime(nullable: false),
                        CalculationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceCalculation", t => t.CalculationId)
                .Index(t => t.CalculationId);
            
            AddColumn("dbo.GlobalProperties", "PriceCalculationsFilesPath", c => c.String());
            AddColumn("dbo.PriceCalculation", "TechnicalRequrementsTask_Id", c => c.Guid());
            CreateIndex("dbo.PriceCalculation", "TechnicalRequrementsTask_Id");
            AddForeignKey("dbo.PriceCalculation", "TechnicalRequrementsTask_Id", "dbo.TechnicalRequrementsTask", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceCalculation", "TechnicalRequrementsTask_Id", "dbo.TechnicalRequrementsTask");
            DropForeignKey("dbo.PriceCalculationFile", "CalculationId", "dbo.PriceCalculation");
            DropIndex("dbo.PriceCalculationFile", new[] { "CalculationId" });
            DropIndex("dbo.PriceCalculation", new[] { "TechnicalRequrementsTask_Id" });
            DropColumn("dbo.PriceCalculation", "TechnicalRequrementsTask_Id");
            DropColumn("dbo.GlobalProperties", "PriceCalculationsFilesPath");
            DropTable("dbo.PriceCalculationFile");
        }
    }
}
