namespace JMWebsite.DAL.JMEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestimonialModelRework : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Testimonial", "CloseState", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Testimonial", "CompName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Testimonial", "CompName", c => c.String(maxLength: 30));
            DropColumn("dbo.Testimonial", "CloseState");
        }
    }
}
