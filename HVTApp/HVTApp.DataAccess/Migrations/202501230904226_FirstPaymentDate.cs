namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstPaymentDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesUnit", "FirstPaymentDate", c => c.DateTime());
            AddColumn("dbo.SalesUnit", "PaidSum", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesUnit", "PaidSum");
            DropColumn("dbo.SalesUnit", "FirstPaymentDate");
        }
    }
}
