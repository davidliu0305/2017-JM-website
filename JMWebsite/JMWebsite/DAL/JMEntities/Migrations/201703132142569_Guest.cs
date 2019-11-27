namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Guest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Guest", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Guest", "MiddleName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Guest", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Guest", "Email", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guest", "Email", c => c.String());
            AlterColumn("dbo.Guest", "LastName", c => c.String());
            AlterColumn("dbo.Guest", "MiddleName", c => c.String());
            AlterColumn("dbo.Guest", "FirstName", c => c.String());
        }
    }
}
