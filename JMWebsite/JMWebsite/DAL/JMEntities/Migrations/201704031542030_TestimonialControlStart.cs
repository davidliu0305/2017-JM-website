namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestimonialControlStart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Testimonial",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        CompName = c.String(maxLength: 30),
                        RecipName = c.String(nullable: false, maxLength: 25),
                        Cat = c.String(nullable: false),
                        Message = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Testimonial");
        }
    }
}
