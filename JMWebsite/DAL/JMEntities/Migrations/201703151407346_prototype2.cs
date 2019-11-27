namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prototype2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Event", "ClientID", "dbo.Client");
            DropForeignKey("dbo.EventImage", "EventID", "dbo.Event");
            AddForeignKey("dbo.Event", "ClientID", "dbo.Client", "ID", cascadeDelete: true);
            AddForeignKey("dbo.EventImage", "EventID", "dbo.Event", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventImage", "EventID", "dbo.Event");
            DropForeignKey("dbo.Event", "ClientID", "dbo.Client");
            AddForeignKey("dbo.EventImage", "EventID", "dbo.Event", "ID");
            AddForeignKey("dbo.Event", "ClientID", "dbo.Client", "ID");
        }
    }
}
