namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contract : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contract", "ContractName", c => c.String());
            DropColumn("dbo.Contract", "FileType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contract", "FileType", c => c.String());
            DropColumn("dbo.Contract", "ContractName");
        }
    }
}
