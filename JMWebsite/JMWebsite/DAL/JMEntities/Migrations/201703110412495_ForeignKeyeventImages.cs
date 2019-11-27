namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyeventImages : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EventImage", new[] { "Event_ID" });
            RenameColumn(table: "dbo.EventImage", name: "Event_ID", newName: "EventID");
            AlterColumn("dbo.EventImage", "EventID", c => c.Int(nullable: false));
            CreateIndex("dbo.EventImage", "EventID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EventImage", new[] { "EventID" });
            AlterColumn("dbo.EventImage", "EventID", c => c.Int());
            RenameColumn(table: "dbo.EventImage", name: "EventID", newName: "Event_ID");
            CreateIndex("dbo.EventImage", "Event_ID");
        }
    }
}
