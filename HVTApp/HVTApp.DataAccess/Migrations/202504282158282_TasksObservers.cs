namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksObservers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DesignDepartmentUser", newName: "DesignDepartmentStaff");
            CreateTable(
                "dbo.DesignDepartmentObservers",
                c => new
                    {
                        DesignDepartment = c.Guid(nullable: false),
                        Observer = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DesignDepartment, t.Observer })
                .ForeignKey("dbo.DesignDepartment", t => t.DesignDepartment, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.Observer, cascadeDelete: true)
                .Index(t => t.DesignDepartment)
                .Index(t => t.Observer);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DesignDepartmentObservers", "Observer", "dbo.User");
            DropForeignKey("dbo.DesignDepartmentObservers", "DesignDepartment", "dbo.DesignDepartment");
            DropIndex("dbo.DesignDepartmentObservers", new[] { "Observer" });
            DropIndex("dbo.DesignDepartmentObservers", new[] { "DesignDepartment" });
            DropTable("dbo.DesignDepartmentObservers");
            RenameTable(name: "dbo.DesignDepartmentStaff", newName: "DesignDepartmentUser");
        }
    }
}
