namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConstructorParameters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConstructorParametersList",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConstructorsParameters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConstructorParametersListParameter",
                c => new
                    {
                        ConstructorParametersList_Id = c.Guid(nullable: false),
                        Parameter_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConstructorParametersList_Id, t.Parameter_Id })
                .ForeignKey("dbo.ConstructorParametersList", t => t.ConstructorParametersList_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parameter", t => t.Parameter_Id, cascadeDelete: true)
                .Index(t => t.ConstructorParametersList_Id)
                .Index(t => t.Parameter_Id);
            
            CreateTable(
                "dbo.ConstructorsParametersUser",
                c => new
                    {
                        ConstructorsParameters_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConstructorsParameters_Id, t.User_Id })
                .ForeignKey("dbo.ConstructorsParameters", t => t.ConstructorsParameters_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.ConstructorsParameters_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ConstructorsParametersConstructorParametersList",
                c => new
                    {
                        ConstructorsParameters_Id = c.Guid(nullable: false),
                        ConstructorParametersList_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConstructorsParameters_Id, t.ConstructorParametersList_Id })
                .ForeignKey("dbo.ConstructorsParameters", t => t.ConstructorsParameters_Id, cascadeDelete: true)
                .ForeignKey("dbo.ConstructorParametersList", t => t.ConstructorParametersList_Id, cascadeDelete: true)
                .Index(t => t.ConstructorsParameters_Id)
                .Index(t => t.ConstructorParametersList_Id);
            
            AddColumn("dbo.GlobalProperties", "LastDeveloperVizit", c => c.DateTime());
            AddColumn("dbo.GlobalProperties", "Developer_Id", c => c.Guid());
            AddColumn("dbo.GlobalProperties", "ProductIncludedDefault_Id", c => c.Guid());
            CreateIndex("dbo.GlobalProperties", "Developer_Id");
            CreateIndex("dbo.GlobalProperties", "ProductIncludedDefault_Id");
            AddForeignKey("dbo.GlobalProperties", "Developer_Id", "dbo.User", "Id");
            AddForeignKey("dbo.GlobalProperties", "ProductIncludedDefault_Id", "dbo.Product", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlobalProperties", "ProductIncludedDefault_Id", "dbo.Product");
            DropForeignKey("dbo.GlobalProperties", "Developer_Id", "dbo.User");
            DropForeignKey("dbo.ConstructorsParametersConstructorParametersList", "ConstructorParametersList_Id", "dbo.ConstructorParametersList");
            DropForeignKey("dbo.ConstructorsParametersConstructorParametersList", "ConstructorsParameters_Id", "dbo.ConstructorsParameters");
            DropForeignKey("dbo.ConstructorsParametersUser", "User_Id", "dbo.User");
            DropForeignKey("dbo.ConstructorsParametersUser", "ConstructorsParameters_Id", "dbo.ConstructorsParameters");
            DropForeignKey("dbo.ConstructorParametersListParameter", "Parameter_Id", "dbo.Parameter");
            DropForeignKey("dbo.ConstructorParametersListParameter", "ConstructorParametersList_Id", "dbo.ConstructorParametersList");
            DropIndex("dbo.ConstructorsParametersConstructorParametersList", new[] { "ConstructorParametersList_Id" });
            DropIndex("dbo.ConstructorsParametersConstructorParametersList", new[] { "ConstructorsParameters_Id" });
            DropIndex("dbo.ConstructorsParametersUser", new[] { "User_Id" });
            DropIndex("dbo.ConstructorsParametersUser", new[] { "ConstructorsParameters_Id" });
            DropIndex("dbo.ConstructorParametersListParameter", new[] { "Parameter_Id" });
            DropIndex("dbo.ConstructorParametersListParameter", new[] { "ConstructorParametersList_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "ProductIncludedDefault_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "Developer_Id" });
            DropColumn("dbo.GlobalProperties", "ProductIncludedDefault_Id");
            DropColumn("dbo.GlobalProperties", "Developer_Id");
            DropColumn("dbo.GlobalProperties", "LastDeveloperVizit");
            DropTable("dbo.ConstructorsParametersConstructorParametersList");
            DropTable("dbo.ConstructorsParametersUser");
            DropTable("dbo.ConstructorParametersListParameter");
            DropTable("dbo.ConstructorsParameters");
            DropTable("dbo.ConstructorParametersList");
        }
    }
}
