namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeClient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Client", "cliPostCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Client", "cliPostCode", c => c.Int(nullable: false));
        }
    }
}
