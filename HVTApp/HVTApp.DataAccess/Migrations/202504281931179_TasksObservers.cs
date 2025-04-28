namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksObservers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "DesignDepartment_Id", c => c.Guid());
            CreateIndex("dbo.User", "DesignDepartment_Id");
            AddForeignKey("dbo.User", "DesignDepartment_Id", "dbo.DesignDepartment", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "DesignDepartment_Id", "dbo.DesignDepartment");
            DropIndex("dbo.User", new[] { "DesignDepartment_Id" });
            DropColumn("dbo.User", "DesignDepartment_Id");
        }
    }
}
