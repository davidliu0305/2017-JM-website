namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "cliName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Client", "cliCity", c => c.String(maxLength: 100));
            AlterColumn("dbo.Client", "cliAddress", c => c.String(maxLength: 100));
            AlterColumn("dbo.Client", "cliPhone", c => c.Long(nullable: false));
            AlterColumn("dbo.Client", "cliEmail", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Client", "cliContactFirst", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Client", "cliContactLast", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Client", "cliContactLast", c => c.String());
            AlterColumn("dbo.Client", "cliContactFirst", c => c.String());
            AlterColumn("dbo.Client", "cliEmail", c => c.String());
            AlterColumn("dbo.Client", "cliPhone", c => c.String());
            AlterColumn("dbo.Client", "cliAddress", c => c.String());
            AlterColumn("dbo.Client", "cliCity", c => c.String());
            AlterColumn("dbo.Client", "cliName", c => c.String());
        }
    }
}
