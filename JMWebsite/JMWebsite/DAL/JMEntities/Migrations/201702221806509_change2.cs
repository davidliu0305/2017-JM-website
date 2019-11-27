namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "cliContactFirst", c => c.String());
            AddColumn("dbo.Client", "cliContactLast", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Client", "cliContactLast");
            DropColumn("dbo.Client", "cliContactFirst");
        }
    }
}
