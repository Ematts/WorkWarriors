namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class range : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompletedBids", "Review", c => c.String());
            AddColumn("dbo.CompletedBids", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompletedBids", "Rating");
            DropColumn("dbo.CompletedBids", "Review");
        }
    }
}
