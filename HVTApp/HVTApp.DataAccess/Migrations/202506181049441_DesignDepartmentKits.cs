namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesignDepartmentKits : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDesignDepartment",
                c => new
                    {
                        Product_Id = c.Guid(nullable: false),
                        DesignDepartment_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.DesignDepartment_Id })
                .ForeignKey("dbo.Product", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.DesignDepartment", t => t.DesignDepartment_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.DesignDepartment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDesignDepartment", "DesignDepartment_Id", "dbo.DesignDepartment");
            DropForeignKey("dbo.ProductDesignDepartment", "Product_Id", "dbo.Product");
            DropIndex("dbo.ProductDesignDepartment", new[] { "DesignDepartment_Id" });
            DropIndex("dbo.ProductDesignDepartment", new[] { "Product_Id" });
            DropTable("dbo.ProductDesignDepartment");
        }
    }
}
