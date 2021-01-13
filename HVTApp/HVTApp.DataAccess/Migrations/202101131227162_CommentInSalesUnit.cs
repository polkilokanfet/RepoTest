namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentInSalesUnit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesUnit", "Comment", c => c.String(maxLength: 150));
            AlterColumn("dbo.EmployeesPosition", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeesPosition", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.SalesUnit", "Comment");
        }
    }
}
