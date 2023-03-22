namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceForPaymentTasks : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TechnicalRequrements", name: "TechnicalRequrementsTask_Id", newName: "TaskId");
            RenameIndex(table: "dbo.TechnicalRequrements", name: "IX_TechnicalRequrementsTask_Id", newName: "IX_TaskId");
            CreateTable(
                "dbo.InvoiceForPaymentTask",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Moment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id", c => c.Guid());
            AddColumn("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id", c => c.Guid());
            CreateIndex("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id");
            CreateIndex("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id");
            AddForeignKey("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id", "dbo.InvoiceForPaymentTask", "Id");
            AddForeignKey("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id", "dbo.InvoiceForPaymentTask", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id", "dbo.InvoiceForPaymentTask");
            DropForeignKey("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id", "dbo.InvoiceForPaymentTask");
            DropIndex("dbo.TechnicalRequrements", new[] { "InvoiceForPaymentTask_Id" });
            DropIndex("dbo.PriceEngineeringTask", new[] { "InvoiceForPaymentTask_Id" });
            DropColumn("dbo.TechnicalRequrements", "InvoiceForPaymentTask_Id");
            DropColumn("dbo.PriceEngineeringTask", "InvoiceForPaymentTask_Id");
            DropTable("dbo.InvoiceForPaymentTask");
            RenameIndex(table: "dbo.TechnicalRequrements", name: "IX_TaskId", newName: "IX_TechnicalRequrementsTask_Id");
            RenameColumn(table: "dbo.TechnicalRequrements", name: "TaskId", newName: "TechnicalRequrementsTask_Id");
        }
    }
}
