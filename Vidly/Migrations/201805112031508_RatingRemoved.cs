namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Rating", c => c.String());
        }
    }
}
