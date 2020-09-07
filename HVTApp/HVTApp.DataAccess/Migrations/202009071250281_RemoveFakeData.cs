namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFakeData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FakeData", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.FakeData", "Id", "dbo.SalesUnit");
            DropIndex("dbo.FakeData", new[] { "Id" });
            DropIndex("dbo.FakeData", new[] { "PaymentConditionSet_Id" });
            AddColumn("dbo.Budget", "DateStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Budget", "DateFinish", c => c.DateTime(nullable: false));
            DropTable("dbo.FakeData");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FakeData",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Cost = c.Double(),
                        RealizationDate = c.DateTime(),
                        OrderInTakeDate = c.DateTime(),
                        PaymentConditionSet_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Budget", "DateFinish");
            DropColumn("dbo.Budget", "DateStart");
            CreateIndex("dbo.FakeData", "PaymentConditionSet_Id");
            CreateIndex("dbo.FakeData", "Id");
            AddForeignKey("dbo.FakeData", "Id", "dbo.SalesUnit", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FakeData", "PaymentConditionSet_Id", "dbo.PaymentConditionSet", "Id");
        }
    }
}
