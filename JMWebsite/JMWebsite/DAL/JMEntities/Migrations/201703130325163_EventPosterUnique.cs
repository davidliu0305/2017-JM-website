namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventPosterUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Event", "PosterName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Event", new[] { "PosterName" });
        }
    }
}
