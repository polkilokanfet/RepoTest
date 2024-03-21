namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBankGuarantee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankGuarantee", "BankGuaranteeType_Id", "dbo.BankGuaranteeType");
            DropForeignKey("dbo.BankGuarantee", "SalesUnit_Id", "dbo.SalesUnit");
            DropIndex("dbo.BankGuarantee", new[] { "BankGuaranteeType_Id" });
            DropIndex("dbo.BankGuarantee", new[] { "SalesUnit_Id" });
            DropTable("dbo.BankGuarantee");
            DropTable("dbo.BankGuaranteeType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BankGuaranteeType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankGuarantee",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Percent = c.Double(nullable: false),
                        Days = c.Int(nullable: false),
                        BankGuaranteeType_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.BankGuarantee", "SalesUnit_Id");
            CreateIndex("dbo.BankGuarantee", "BankGuaranteeType_Id");
            AddForeignKey("dbo.BankGuarantee", "SalesUnit_Id", "dbo.SalesUnit", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BankGuarantee", "BankGuaranteeType_Id", "dbo.BankGuaranteeType", "Id");
        }
    }
}
