namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecificationUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "Specification_Id", c => c.Guid());
            AddColumn("dbo.TechnicalRequrements", "Specification_Id", c => c.Guid());
            AlterColumn("dbo.TaskInvoiceForPayment", "MomentStart", c => c.DateTime());
            CreateIndex("dbo.PriceEngineeringTask", "Specification_Id");
            CreateIndex("dbo.TechnicalRequrements", "Specification_Id");
            AddForeignKey("dbo.PriceEngineeringTask", "Specification_Id", "dbo.Specification", "Id");
            AddForeignKey("dbo.TechnicalRequrements", "Specification_Id", "dbo.Specification", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TechnicalRequrements", "Specification_Id", "dbo.Specification");
            DropForeignKey("dbo.PriceEngineeringTask", "Specification_Id", "dbo.Specification");
            DropIndex("dbo.TechnicalRequrements", new[] { "Specification_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "Specification_Id" });
            AlterColumn("dbo.TaskInvoiceForPayment", "MomentStart", c => c.DateTime(nullable: false));
            DropColumn("dbo.TechnicalRequrements", "Specification_Id");
            DropColumn("dbo.PriceEngineeringTask", "Specification_Id");
        }
    }
}
