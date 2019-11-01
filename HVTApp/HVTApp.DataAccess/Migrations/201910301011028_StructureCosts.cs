namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StructureCosts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StructureCosts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StructureCost",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 50),
                        Amount = c.Double(nullable: false),
                        Comment = c.String(maxLength: 200),
                        StructureCosts_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StructureCosts", t => t.StructureCosts_Id, cascadeDelete: true)
                .Index(t => t.StructureCosts_Id);
            
            AddColumn("dbo.SalesUnit", "StructureCosts_Id", c => c.Guid());
            CreateIndex("dbo.SalesUnit", "StructureCosts_Id");
            AddForeignKey("dbo.SalesUnit", "StructureCosts_Id", "dbo.StructureCosts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesUnit", "StructureCosts_Id", "dbo.StructureCosts");
            DropForeignKey("dbo.StructureCost", "StructureCosts_Id", "dbo.StructureCosts");
            DropIndex("dbo.StructureCost", new[] { "StructureCosts_Id" });
            DropIndex("dbo.SalesUnit", new[] { "StructureCosts_Id" });
            DropColumn("dbo.SalesUnit", "StructureCosts_Id");
            DropTable("dbo.StructureCost");
            DropTable("dbo.StructureCosts");
        }
    }
}
