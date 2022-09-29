namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentsId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PaymentActual", name: "SalesUnit_Id", newName: "SalesUnitId");
            RenameColumn(table: "dbo.PaymentActual", name: "PaymentDocument_Id", newName: "PaymentDocumentId");
            RenameIndex(table: "dbo.PaymentActual", name: "IX_SalesUnit_Id", newName: "IX_SalesUnitId");
            RenameIndex(table: "dbo.PaymentActual", name: "IX_PaymentDocument_Id", newName: "IX_PaymentDocumentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PaymentActual", name: "IX_PaymentDocumentId", newName: "IX_PaymentDocument_Id");
            RenameIndex(table: "dbo.PaymentActual", name: "IX_SalesUnitId", newName: "IX_SalesUnit_Id");
            RenameColumn(table: "dbo.PaymentActual", name: "PaymentDocumentId", newName: "PaymentDocument_Id");
            RenameColumn(table: "dbo.PaymentActual", name: "SalesUnitId", newName: "SalesUnit_Id");
        }
    }
}
