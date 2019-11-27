namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11101 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Event", new[] { "PosterName" });
            AlterColumn("dbo.Event", "PosterName", c => c.String(maxLength: 20));
            CreateIndex("dbo.Event", "PosterName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Event", new[] { "PosterName" });
            AlterColumn("dbo.Event", "PosterName", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Event", "PosterName", unique: true);
        }
    }
}
