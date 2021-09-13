namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacilityAddressIsRequared2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Facility", new[] { "Address_Id" });
            AlterColumn("dbo.Facility", "Address_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Facility", "Address_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Facility", new[] { "Address_Id" });
            AlterColumn("dbo.Facility", "Address_Id", c => c.Guid());
            CreateIndex("dbo.Facility", "Address_Id");
        }
    }
}
