namespace HVTApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteProjectId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Note", new[] { "Project_Id" });
            RenameColumn(table: "dbo.Note", name: "Project_Id", newName: "ProjectId");
            AlterColumn("dbo.Note", "ProjectId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Note", "ProjectId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Note", new[] { "ProjectId" });
            AlterColumn("dbo.Note", "ProjectId", c => c.Guid());
            RenameColumn(table: "dbo.Note", name: "ProjectId", newName: "Project_Id");
            CreateIndex("dbo.Note", "Project_Id");
        }
    }
}
