namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Budget : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budget",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BudgetUnit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderInTakeDate = c.DateTime(nullable: false),
                        RealizationDate = c.DateTime(nullable: false),
                        OrderInTakeDateByManager = c.DateTime(nullable: false),
                        RealizationDateByManager = c.DateTime(nullable: false),
                        Cost = c.Double(nullable: false),
                        CostByManager = c.Double(nullable: false),
                        PaymentConditionSet_Id = c.Guid(nullable: false),
                        PaymentConditionSetByManager_Id = c.Guid(nullable: false),
                        SalesUnit_Id = c.Guid(nullable: false),
                        Budget_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentConditionSet", t => t.PaymentConditionSet_Id)
                .ForeignKey("dbo.PaymentConditionSet", t => t.PaymentConditionSetByManager_Id)
                .ForeignKey("dbo.SalesUnit", t => t.SalesUnit_Id, cascadeDelete: true)
                .ForeignKey("dbo.Budget", t => t.Budget_Id, cascadeDelete: true)
                .Index(t => t.PaymentConditionSet_Id)
                .Index(t => t.PaymentConditionSetByManager_Id)
                .Index(t => t.SalesUnit_Id)
                .Index(t => t.Budget_Id);
            
            AddColumn("dbo.SalesUnit", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BudgetUnit", "Budget_Id", "dbo.Budget");
            DropForeignKey("dbo.BudgetUnit", "SalesUnit_Id", "dbo.SalesUnit");
            DropForeignKey("dbo.BudgetUnit", "PaymentConditionSetByManager_Id", "dbo.PaymentConditionSet");
            DropForeignKey("dbo.BudgetUnit", "PaymentConditionSet_Id", "dbo.PaymentConditionSet");
            DropIndex("dbo.BudgetUnit", new[] { "Budget_Id" });
            DropIndex("dbo.BudgetUnit", new[] { "SalesUnit_Id" });
            DropIndex("dbo.BudgetUnit", new[] { "PaymentConditionSetByManager_Id" });
            DropIndex("dbo.BudgetUnit", new[] { "PaymentConditionSet_Id" });
            DropColumn("dbo.SalesUnit", "IsRemoved");
            DropTable("dbo.BudgetUnit");
            DropTable("dbo.Budget");
        }
    }
}
