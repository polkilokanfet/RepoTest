namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesUnitProductId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SalesUnit", "Project_Id", "dbo.Project");
            DropIndex("dbo.Project", new[] { "ProjectType_Id" });
            RenameColumn(table: "dbo.SalesUnit", name: "Facility_Id", newName: "FacilityId");
            RenameColumn(table: "dbo.SalesUnit", name: "PaymentConditionSet_Id", newName: "PaymentConditionSetId");
            RenameColumn(table: "dbo.SalesUnit", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.SalesUnit", name: "Project_Id", newName: "ProjectId");
            RenameColumn(table: "dbo.Project", name: "Manager_Id", newName: "ManagerId");
            RenameColumn(table: "dbo.Project", name: "ProjectType_Id", newName: "ProjectTypeId");
            RenameIndex(table: "dbo.SalesUnit", name: "IX_Facility_Id", newName: "IX_FacilityId");
            RenameIndex(table: "dbo.SalesUnit", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameIndex(table: "dbo.SalesUnit", name: "IX_PaymentConditionSet_Id", newName: "IX_PaymentConditionSetId");
            RenameIndex(table: "dbo.SalesUnit", name: "IX_Project_Id", newName: "IX_ProjectId");
            RenameIndex(table: "dbo.Project", name: "IX_Manager_Id", newName: "IX_ManagerId");
            AlterColumn("dbo.Project", "ProjectTypeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Project", "ProjectTypeId");
            AddForeignKey("dbo.SalesUnit", "ProjectId", "dbo.Project", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesUnit", "ProjectId", "dbo.Project");
            DropIndex("dbo.Project", new[] { "ProjectTypeId" });
            AlterColumn("dbo.Project", "ProjectTypeId", c => c.Guid());
            RenameIndex(table: "dbo.Project", name: "IX_ManagerId", newName: "IX_Manager_Id");
            RenameIndex(table: "dbo.SalesUnit", name: "IX_ProjectId", newName: "IX_Project_Id");
            RenameIndex(table: "dbo.SalesUnit", name: "IX_PaymentConditionSetId", newName: "IX_PaymentConditionSet_Id");
            RenameIndex(table: "dbo.SalesUnit", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameIndex(table: "dbo.SalesUnit", name: "IX_FacilityId", newName: "IX_Facility_Id");
            RenameColumn(table: "dbo.Project", name: "ProjectTypeId", newName: "ProjectType_Id");
            RenameColumn(table: "dbo.Project", name: "ManagerId", newName: "Manager_Id");
            RenameColumn(table: "dbo.SalesUnit", name: "ProjectId", newName: "Project_Id");
            RenameColumn(table: "dbo.SalesUnit", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.SalesUnit", name: "PaymentConditionSetId", newName: "PaymentConditionSet_Id");
            RenameColumn(table: "dbo.SalesUnit", name: "FacilityId", newName: "Facility_Id");
            CreateIndex("dbo.Project", "ProjectType_Id");
            AddForeignKey("dbo.SalesUnit", "Project_Id", "dbo.Project", "Id", cascadeDelete: true);
        }
    }
}
