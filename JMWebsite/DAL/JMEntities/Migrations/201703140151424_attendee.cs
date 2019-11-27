namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attendee", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Attendee", "MiddleName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Attendee", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Attendee", "Role", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Attendee", "Email", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attendee", "Email", c => c.String());
            AlterColumn("dbo.Attendee", "Role", c => c.String());
            AlterColumn("dbo.Attendee", "LastName", c => c.String());
            AlterColumn("dbo.Attendee", "MiddleName", c => c.String());
            AlterColumn("dbo.Attendee", "FirstName", c => c.String());
        }
    }
}
