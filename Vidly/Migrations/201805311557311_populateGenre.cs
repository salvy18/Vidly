namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GENRES ( name) VALUES ('Comedy')");
            Sql("INSERT INTO GENRES ( name) VALUES ('Action')");
            Sql("INSERT INTO GENRES ( name) VALUES ('Family')");
            Sql("INSERT INTO GENRES ( name) VALUES ('Romance')");

        }
        
        public override void Down()
        {
        }
    }
}
