namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CateringService", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Schedule", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Schedule", "Description", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schedule", "Description", c => c.String());
            AlterColumn("dbo.Schedule", "Name", c => c.String());
            AlterColumn("dbo.CateringService", "Name", c => c.Int(nullable: false));
        }
    }
}
