namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompletedBids", "HomeownerComfirmedBidsID", c => c.Int(nullable: false));
            CreateIndex("dbo.CompletedBids", "HomeownerComfirmedBidsID");
            AddForeignKey("dbo.CompletedBids", "HomeownerComfirmedBidsID", "dbo.HomeownerComfirmedBids", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompletedBids", "HomeownerComfirmedBidsID", "dbo.HomeownerComfirmedBids");
            DropIndex("dbo.CompletedBids", new[] { "HomeownerComfirmedBidsID" });
            DropColumn("dbo.CompletedBids", "HomeownerComfirmedBidsID");
        }
    }
}
