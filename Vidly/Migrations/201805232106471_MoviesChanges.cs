namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoviesChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Genre", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Movies", "Released_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "Date_Added", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "Number_In_Stock ", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Number_In_Stock ");
            DropColumn("dbo.Movies", "Date_Added");
            DropColumn("dbo.Movies", "Released_Date");
            DropColumn("dbo.Movies", "Genre");
        }
    }
}
