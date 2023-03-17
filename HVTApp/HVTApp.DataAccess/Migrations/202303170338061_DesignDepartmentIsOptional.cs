namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesignDepartmentIsOptional : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PriceEngineeringTask", new[] { "DesignDepartment_Id" });
            AlterColumn("dbo.PriceEngineeringTask", "DesignDepartment_Id", c => c.Guid());
            CreateIndex("dbo.PriceEngineeringTask", "DesignDepartment_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PriceEngineeringTask", new[] { "DesignDepartment_Id" });
            AlterColumn("dbo.PriceEngineeringTask", "DesignDepartment_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.PriceEngineeringTask", "DesignDepartment_Id");
        }
    }
}
