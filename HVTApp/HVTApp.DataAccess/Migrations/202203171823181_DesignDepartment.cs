namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesignDepartment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DesignDepartment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 120),
                        Head_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Head_Id)
                .Index(t => t.Head_Id);
            
            CreateTable(
                "dbo.DesignDepartmentParameters",
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
                "dbo.DesignDepartmentParametersParameter",
                c => new
                    {
                        DesignDepartmentParameters_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DesignDepartmentParameters_Id, t.Parameter_Id })
                .ForeignKey("dbo.DesignDepartmentParameters", t => t.DesignDepartmentParameters_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.DesignDepartmentParameters_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.DesignDepartmentUser",
                c => new
                    {
                        DesignDepartment_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DesignDepartment_Id, t.User_Id })
                .ForeignKey("dbo.DesignDepartment", t => t.DesignDepartment_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.DesignDepartment_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DesignDepartmentUser", "User_Id", "dbo.User");
            DropForeignKey("dbo.DesignDepartmentUser", "DesignDepartment_Id", "dbo.DesignDepartment");
            DropForeignKey("dbo.DesignDepartmentParameters", "DesignDepartmentId", "dbo.DesignDepartment");
            DropForeignKey("dbo.DesignDepartmentParametersParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.DesignDepartmentParametersParameter", "DesignDepartmentParameters_Id", "dbo.DesignDepartmentParameters");
            DropForeignKey("dbo.DesignDepartment", "Head_Id", "dbo.User");
            DropIndex("dbo.DesignDepartmentUser", new[] { "User_Id" });
            DropIndex("dbo.DesignDepartmentUser", new[] { "DesignDepartment_Id" });
            DropIndex("dbo.DesignDepartmentParametersParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.DesignDepartmentParametersParameter", new[] { "DesignDepartmentParameters_Id" });
            DropIndex("dbo.DesignDepartmentParameters", new[] { "DesignDepartmentId" });
            DropIndex("dbo.DesignDepartment", new[] { "Head_Id" });
            DropTable("dbo.DesignDepartmentUser");
            DropTable("dbo.DesignDepartmentParametersParameter");
            DropTable("dbo.DesignDepartmentParameters");
            DropTable("dbo.DesignDepartment");
        }
    }
}
