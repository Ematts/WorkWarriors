namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "HomeownerID", c => c.Int());
            CreateIndex("dbo.Addresses", "HomeownerID");
            AddForeignKey("dbo.Addresses", "HomeownerID", "dbo.Homeowners", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "HomeownerID", "dbo.Homeowners");
            DropIndex("dbo.Addresses", new[] { "HomeownerID" });
            DropColumn("dbo.Addresses", "HomeownerID");
        }
    }
}
