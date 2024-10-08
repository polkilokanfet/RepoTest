namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlanMakerInTaskInvoiceForPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskInvoiceForPayment", "MomentFinishByPlanMaker", c => c.DateTime());
            AddColumn("dbo.TaskInvoiceForPayment", "PlanMaker_Id", c => c.Guid());
            CreateIndex("dbo.TaskInvoiceForPayment", "PlanMaker_Id");
            AddForeignKey("dbo.TaskInvoiceForPayment", "PlanMaker_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskInvoiceForPayment", "PlanMaker_Id", "dbo.User");
            DropIndex("dbo.TaskInvoiceForPayment", new[] { "PlanMaker_Id" });
            DropColumn("dbo.TaskInvoiceForPayment", "PlanMaker_Id");
            DropColumn("dbo.TaskInvoiceForPayment", "MomentFinishByPlanMaker");
        }
    }
}
