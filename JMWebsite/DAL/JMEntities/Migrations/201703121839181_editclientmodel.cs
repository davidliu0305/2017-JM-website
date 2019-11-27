namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editclientmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "cliContactFirst", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Client", "cliContactLast", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Client", "cliContactLast", c => c.String());
            AlterColumn("dbo.Client", "cliContactFirst", c => c.String());
        }
    }
}
