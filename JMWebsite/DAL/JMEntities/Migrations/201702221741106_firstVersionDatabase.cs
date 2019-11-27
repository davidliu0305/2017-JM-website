namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstVersionDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Role = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        ActCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AttendeeDinner = c.Boolean(nullable: false),
                        AlcService = c.Boolean(nullable: false),
                        AntipastoBar = c.Boolean(nullable: false),
                        fPayName = c.String(),
                        fPayDate = c.DateTime(nullable: false),
                        ClientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Client", t => t.ClientID)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.CateringService",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        Name = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        cliName = c.String(),
                        cliCity = c.String(),
                        cliAddress = c.String(),
                        cliPhone = c.String(),
                        cliEmail = c.String(),
                        cliPostCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        FileType = c.String(),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.EventImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Decription = c.String(),
                        FileType = c.String(),
                        Event_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Event", t => t.Event_ID)
                .Index(t => t.Event_ID);
            
            CreateTable(
                "dbo.Guest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Entree = c.Boolean(nullable: false),
                        AgeTitle = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Decription = c.String(),
                        FileType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventAttendee",
                c => new
                    {
                        Event_ID = c.Int(nullable: false),
                        Attendee_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_ID, t.Attendee_ID })
                .ForeignKey("dbo.Event", t => t.Event_ID, cascadeDelete: true)
                .ForeignKey("dbo.Attendee", t => t.Attendee_ID, cascadeDelete: true)
                .Index(t => t.Event_ID)
                .Index(t => t.Attendee_ID);
            
            CreateTable(
                "dbo.GuestEvent",
                c => new
                    {
                        Guest_ID = c.Int(nullable: false),
                        Event_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Guest_ID, t.Event_ID })
                .ForeignKey("dbo.Guest", t => t.Guest_ID, cascadeDelete: true)
                .ForeignKey("dbo.Event", t => t.Event_ID, cascadeDelete: true)
                .Index(t => t.Guest_ID)
                .Index(t => t.Event_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedule", "EventID", "dbo.Event");
            DropForeignKey("dbo.GuestEvent", "Event_ID", "dbo.Event");
            DropForeignKey("dbo.GuestEvent", "Guest_ID", "dbo.Guest");
            DropForeignKey("dbo.EventImage", "Event_ID", "dbo.Event");
            DropForeignKey("dbo.Contract", "EventID", "dbo.Event");
            DropForeignKey("dbo.Event", "ClientID", "dbo.Client");
            DropForeignKey("dbo.CateringService", "EventID", "dbo.Event");
            DropForeignKey("dbo.EventAttendee", "Attendee_ID", "dbo.Attendee");
            DropForeignKey("dbo.EventAttendee", "Event_ID", "dbo.Event");
            DropIndex("dbo.GuestEvent", new[] { "Event_ID" });
            DropIndex("dbo.GuestEvent", new[] { "Guest_ID" });
            DropIndex("dbo.EventAttendee", new[] { "Attendee_ID" });
            DropIndex("dbo.EventAttendee", new[] { "Event_ID" });
            DropIndex("dbo.Schedule", new[] { "EventID" });
            DropIndex("dbo.EventImage", new[] { "Event_ID" });
            DropIndex("dbo.Contract", new[] { "EventID" });
            DropIndex("dbo.CateringService", new[] { "EventID" });
            DropIndex("dbo.Event", new[] { "ClientID" });
            DropTable("dbo.GuestEvent");
            DropTable("dbo.EventAttendee");
            DropTable("dbo.Image");
            DropTable("dbo.Schedule");
            DropTable("dbo.Guest");
            DropTable("dbo.EventImage");
            DropTable("dbo.Contract");
            DropTable("dbo.Client");
            DropTable("dbo.CateringService");
            DropTable("dbo.Event");
            DropTable("dbo.Attendee");
        }
    }
}
