namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueContract : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contract", "ContractName", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Contract", "ContractName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contract", new[] { "ContractName" });
            AlterColumn("dbo.Contract", "ContractName", c => c.String());
        }
    }
}
