namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventPosterMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "PosterName", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "PosterName", c => c.String());
        }
    }
}
