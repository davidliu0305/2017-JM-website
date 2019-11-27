namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventmigra : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Event", "Description", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "Description", c => c.String());
            AlterColumn("dbo.Event", "Name", c => c.String(nullable: false));
        }
    }
}
