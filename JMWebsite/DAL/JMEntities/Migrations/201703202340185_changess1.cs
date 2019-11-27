namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changess1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CateringService", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Schedule", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schedule", "Description", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.CateringService", "Description");
        }
    }
}
