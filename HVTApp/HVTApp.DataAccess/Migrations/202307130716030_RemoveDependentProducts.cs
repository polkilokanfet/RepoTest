namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDependentProducts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductDependent", "MainProductId", "dbo.Product");
            AddForeignKey("dbo.ProductDependent", "MainProductId", "dbo.Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDependent", "MainProductId", "dbo.Product");
            AddForeignKey("dbo.ProductDependent", "MainProductId", "dbo.Product", "Id");
        }
    }
}
