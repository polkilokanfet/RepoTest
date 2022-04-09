namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesignDepartmentParametersAddedBlocks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DesignDepartmentParametersAddedBlocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DesignDepartmentId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DesignDepartment", t => t.DesignDepartmentId)
                .Index(t => t.DesignDepartmentId);
            
            CreateTable(
                "dbo.DesignDepartmentParametersAddedBlocksParameter",
                c => new
                    {
                        DesignDepartmentParametersAddedBlocks_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DesignDepartmentParametersAddedBlocks_Id, t.Parameter_Id })
                .ForeignKey("dbo.DesignDepartmentParametersAddedBlocks", t => t.DesignDepartmentParametersAddedBlocks_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.DesignDepartmentParametersAddedBlocks_Id)
                .Index(t => t.Parameter_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DesignDepartmentParametersAddedBlocks", "DesignDepartmentId", "dbo.DesignDepartment");
            DropForeignKey("dbo.DesignDepartmentParametersAddedBlocksParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.DesignDepartmentParametersAddedBlocksParameter", "DesignDepartmentParametersAddedBlocks_Id", "dbo.DesignDepartmentParametersAddedBlocks");
            DropIndex("dbo.DesignDepartmentParametersAddedBlocksParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.DesignDepartmentParametersAddedBlocksParameter", new[] { "DesignDepartmentParametersAddedBlocks_Id" });
            DropIndex("dbo.DesignDepartmentParametersAddedBlocks", new[] { "DesignDepartmentId" });
            DropTable("dbo.DesignDepartmentParametersAddedBlocksParameter");
            DropTable("dbo.DesignDepartmentParametersAddedBlocks");
        }
    }
}
