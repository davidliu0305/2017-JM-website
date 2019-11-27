namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventPoster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "PosterName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "PosterName");
        }
    }
}
