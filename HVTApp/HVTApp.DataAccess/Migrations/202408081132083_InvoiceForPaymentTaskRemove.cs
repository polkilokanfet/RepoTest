namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceForPaymentTaskRemove : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id", "dbo.InvoiceForPaymentTask");
            DropForeignKey("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id", "dbo.InvoiceForPaymentTask");
            DropIndex("dbo.PriceEngineeringTask", new[] { "InvoiceForPaymentTask_Id" });
            DropIndex("dbo.TechnicalRequrements", new[] { "InvoiceForPaymentTask_Id" });
            DropColumn("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id");
            DropColumn("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id");
            DropTable("dbo.InvoiceForPaymentTask");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InvoiceForPaymentTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id", c => c.Guid());
            AddColumn("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id", c => c.Guid());
            CreateIndex("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id");
            CreateIndex("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id");
            AddForeignKey("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id", "dbo.InvoiceForPaymentTask", "Id");
            AddForeignKey("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id", "dbo.InvoiceForPaymentTask", "Id");
        }
    }
}
