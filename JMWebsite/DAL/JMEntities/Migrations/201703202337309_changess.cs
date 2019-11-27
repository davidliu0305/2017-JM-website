namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changess : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CateringService", "Name");
            DropColumn("dbo.CateringService", "Time");
            DropColumn("dbo.Schedule", "Name");
            DropColumn("dbo.Schedule", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedule", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Schedule", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.CateringService", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.CateringService", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
