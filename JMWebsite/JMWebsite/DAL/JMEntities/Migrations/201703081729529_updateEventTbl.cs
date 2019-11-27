namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEventTbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Event", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "Name", c => c.String());
            AlterColumn("dbo.Event", "Type", c => c.String());
        }
    }
}
