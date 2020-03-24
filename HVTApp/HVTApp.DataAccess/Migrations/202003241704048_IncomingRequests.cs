namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncomingRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncomingRequest",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Document", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.IncomingRequestEmployee",
                c => new
                    {
                        IncomingRequest_Id = c.Guid(nullable: false),
                        Employee_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IncomingRequest_Id, t.Employee_Id })
                .ForeignKey("dbo.IncomingRequest", t => t.IncomingRequest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.IncomingRequest_Id)
                .Index(t => t.Employee_Id);
            
            AddColumn("dbo.GlobalProperties", "IncomingRequestsPath", c => c.String());
            AlterColumn("dbo.ProductType", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncomingRequestEmployee", "Employee_Id", "dbo.Employee");
            DropForeignKey("dbo.IncomingRequestEmployee", "IncomingRequest_Id", "dbo.IncomingRequest");
            DropForeignKey("dbo.IncomingRequest", "Id", "dbo.Document");
            DropIndex("dbo.IncomingRequestEmployee", new[] { "Employee_Id" });
            DropIndex("dbo.IncomingRequestEmployee", new[] { "IncomingRequest_Id" });
            DropIndex("dbo.IncomingRequest", new[] { "Id" });
            AlterColumn("dbo.ProductType", "Name", c => c.String(nullable: false, maxLength: 75));
            DropColumn("dbo.GlobalProperties", "IncomingRequestsPath");
            DropTable("dbo.IncomingRequestEmployee");
            DropTable("dbo.IncomingRequest");
        }
    }
}
