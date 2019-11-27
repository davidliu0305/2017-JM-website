namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeValidations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Event", "ClientID", "dbo.Client");
            DropForeignKey("dbo.EventImage", "EventID", "dbo.Event");
            AlterColumn("dbo.CateringService", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Schedule", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Schedule", "Description", c => c.String(nullable: false, maxLength: 300));
            AddForeignKey("dbo.Event", "ClientID", "dbo.Client", "ID");
            AddForeignKey("dbo.EventImage", "EventID", "dbo.Event", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventImage", "EventID", "dbo.Event");
            DropForeignKey("dbo.Event", "ClientID", "dbo.Client");
            AlterColumn("dbo.Schedule", "Description", c => c.String());
            AlterColumn("dbo.Schedule", "Name", c => c.String());
            AlterColumn("dbo.CateringService", "Name", c => c.Int(nullable: false));
            AddForeignKey("dbo.EventImage", "EventID", "dbo.Event", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Event", "ClientID", "dbo.Client", "ID", cascadeDelete: true);
        }
    }
}
