namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesignDocumentationAvailability : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceEngineeringTask", "NeedDesignDocumentationDevelopment", c => c.Boolean(nullable: false));
            AddColumn("dbo.PriceEngineeringTask", "DaysToDesignDocumentationDevelopment", c => c.Short());
            AddColumn("dbo.PriceEngineeringTask", "DesignDocumentationAvailabilityComment", c => c.String(maxLength: 512));
            AddColumn("dbo.PriceEngineeringTask", "NeedEquipment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceEngineeringTask", "NeedEquipment");
            DropColumn("dbo.PriceEngineeringTask", "DesignDocumentationAvailabilityComment");
            DropColumn("dbo.PriceEngineeringTask", "DaysToDesignDocumentationDevelopment");
            DropColumn("dbo.PriceEngineeringTask", "NeedDesignDocumentationDevelopment");
        }
    }
}
