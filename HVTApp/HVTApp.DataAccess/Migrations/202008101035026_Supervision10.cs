namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Supervision10 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GlobalProperties", new[] { "ComplectDesignationGroup_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "ComplectsGroup_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "ComplectsParameter_Id" });
            CreateTable(
                "dbo.Supervision",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateFinish = c.DateTime(),
                        DateRequired = c.DateTime(),
                        ClientOrderNumber = c.String(maxLength: 25),
                        ServiceOrderNumber = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SalesUnit", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.GlobalProperties", "RecipientSupervisionLetterEmployee_Id", c => c.Guid());
            AddColumn("dbo.SalesUnit", "Supervision_Id", c => c.Guid());
            AlterColumn("dbo.GlobalProperties", "ComplectDesignationGroup_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.GlobalProperties", "ComplectsGroup_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.GlobalProperties", "ComplectsParameter_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.GlobalProperties", "ComplectDesignationGroup_Id");
            CreateIndex("dbo.GlobalProperties", "ComplectsGroup_Id");
            CreateIndex("dbo.GlobalProperties", "ComplectsParameter_Id");
            CreateIndex("dbo.GlobalProperties", "RecipientSupervisionLetterEmployee_Id");
            CreateIndex("dbo.SalesUnit", "Supervision_Id");
            AddForeignKey("dbo.GlobalProperties", "RecipientSupervisionLetterEmployee_Id", "dbo.Employee", "Id");
            AddForeignKey("dbo.SalesUnit", "Supervision_Id", "dbo.Supervision", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesUnit", "Supervision_Id", "dbo.Supervision");
            DropForeignKey("dbo.Supervision", "Id", "dbo.SalesUnit");
            DropForeignKey("dbo.GlobalProperties", "RecipientSupervisionLetterEmployee_Id", "dbo.Employee");
            DropIndex("dbo.Supervision", new[] { "Id" });
            DropIndex("dbo.SalesUnit", new[] { "Supervision_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "RecipientSupervisionLetterEmployee_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "ComplectsParameter_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "ComplectsGroup_Id" });
            DropIndex("dbo.GlobalProperties", new[] { "ComplectDesignationGroup_Id" });
            AlterColumn("dbo.GlobalProperties", "ComplectsParameter_Id", c => c.Guid());
            AlterColumn("dbo.GlobalProperties", "ComplectsGroup_Id", c => c.Guid());
            AlterColumn("dbo.GlobalProperties", "ComplectDesignationGroup_Id", c => c.Guid());
            DropColumn("dbo.SalesUnit", "Supervision_Id");
            DropColumn("dbo.GlobalProperties", "RecipientSupervisionLetterEmployee_Id");
            DropTable("dbo.Supervision");
            CreateIndex("dbo.GlobalProperties", "ComplectsParameter_Id");
            CreateIndex("dbo.GlobalProperties", "ComplectsGroup_Id");
            CreateIndex("dbo.GlobalProperties", "ComplectDesignationGroup_Id");
        }
    }
}
