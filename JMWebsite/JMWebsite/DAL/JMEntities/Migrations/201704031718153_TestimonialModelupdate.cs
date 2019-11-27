namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestimonialModelupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Testimonial", "Poster", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Testimonial", "Poster");
        }
    }
}
