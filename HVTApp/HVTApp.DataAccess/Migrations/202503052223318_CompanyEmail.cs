namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Company", "Email", c => c.String(maxLength: 124));
            AddColumn("dbo.Contract", "ContragentEmployee_Id", c => c.Guid());
            CreateIndex("dbo.Contract", "ContragentEmployee_Id");
            AddForeignKey("dbo.Contract", "ContragentEmployee_Id", "dbo.Employee", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contract", "ContragentEmployee_Id", "dbo.Employee");
            DropIndex("dbo.Contract", new[] { "ContragentEmployee_Id" });
            DropColumn("dbo.Contract", "ContragentEmployee_Id");
            DropColumn("dbo.Company", "Email");
        }
    }
}
