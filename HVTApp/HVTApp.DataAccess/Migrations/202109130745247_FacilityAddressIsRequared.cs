namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacilityAddressIsRequared : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SalesUnit", "Facility_Id", "dbo.Facility");
            DropForeignKey("dbo.OfferUnit", "Facility_Id", "dbo.Facility");
            DropIndex("dbo.Facility", new[] { "Address_Id" });
            DropColumn("dbo.Facility", "Id");
            RenameColumn(table: "dbo.Facility", name: "Address_Id", newName: "Id");
            DropPrimaryKey("dbo.Facility");
            AlterColumn("dbo.Facility", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Facility", "Id");
            CreateIndex("dbo.Facility", "Id");
            AddForeignKey("dbo.SalesUnit", "Facility_Id", "dbo.Facility", "Id");
            AddForeignKey("dbo.OfferUnit", "Facility_Id", "dbo.Facility", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OfferUnit", "Facility_Id", "dbo.Facility");
            DropForeignKey("dbo.SalesUnit", "Facility_Id", "dbo.Facility");
            DropIndex("dbo.Facility", new[] { "Id" });
            DropPrimaryKey("dbo.Facility");
            AlterColumn("dbo.Facility", "Id", c => c.Guid());
            AddPrimaryKey("dbo.Facility", "Id");
            RenameColumn(table: "dbo.Facility", name: "Id", newName: "Address_Id");
            AddColumn("dbo.Facility", "Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Facility", "Address_Id");
            AddForeignKey("dbo.OfferUnit", "Facility_Id", "dbo.Facility", "Id");
            AddForeignKey("dbo.SalesUnit", "Facility_Id", "dbo.Facility", "Id");
        }
    }
}
