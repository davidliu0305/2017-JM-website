namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestimonialModelFinal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Testimonial", "Message", c => c.String(nullable: false, maxLength: 1200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Testimonial", "Message", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
