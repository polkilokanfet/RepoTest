namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskInvoiceForPayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskInvoiceForPayment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MomentStart = c.DateTime(nullable: false),
                        MomentFinish = c.DateTime(),
                        OriginalIsRequired = c.Boolean(nullable: false),
                        Comment = c.String(maxLength: 128),
                        BackManager_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.BackManager_Id)
                .Index(t => t.BackManager_Id);
            
            CreateTable(
                "dbo.TaskInvoiceForPaymentItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskId = c.Guid(nullable: false),
                        PaymentCondition_Id = c.Guid(nullable: false),
                        PriceEngineeringTask_Id = c.Guid(),
                        TechnicalRequrements_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentCondition", t => t.PaymentCondition_Id)
                .ForeignKey("dbo.PriceEngineeringTask", t => t.PriceEngineeringTask_Id)
                .ForeignKey("dbo.TechnicalRequrements", t => t.TechnicalRequrements_Id)
                .ForeignKey("dbo.TaskInvoiceForPayment", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.PaymentCondition_Id)
                .Index(t => t.PriceEngineeringTask_Id)
                .Index(t => t.TechnicalRequrements_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskInvoiceForPaymentItem", "TaskId", "dbo.TaskInvoiceForPayment");
            DropForeignKey("dbo.TaskInvoiceForPaymentItem", "TechnicalRequrements_Id", "dbo.TechnicalRequrements");
            DropForeignKey("dbo.TaskInvoiceForPaymentItem", "PriceEngineeringTask_Id", "dbo.PriceEngineeringTask");
            DropForeignKey("dbo.TaskInvoiceForPaymentItem", "PaymentCondition_Id", "dbo.PaymentCondition");
            DropForeignKey("dbo.TaskInvoiceForPayment", "BackManager_Id", "dbo.User");
            DropIndex("dbo.TaskInvoiceForPaymentItem", new[] { "TechnicalRequrements_Id" });
            DropIndex("dbo.TaskInvoiceForPaymentItem", new[] { "PriceEngineeringTask_Id" });
            DropIndex("dbo.TaskInvoiceForPaymentItem", new[] { "PaymentCondition_Id" });
            DropIndex("dbo.TaskInvoiceForPaymentItem", new[] { "TaskId" });
            DropIndex("dbo.TaskInvoiceForPayment", new[] { "BackManager_Id" });
            DropTable("dbo.TaskInvoiceForPaymentItem");
            DropTable("dbo.TaskInvoiceForPayment");
        }
    }
}
