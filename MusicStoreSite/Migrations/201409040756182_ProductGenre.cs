namespace MusicStoreSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductGenre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "GenreId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Genre", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "GenreId");
        }
    }
}
