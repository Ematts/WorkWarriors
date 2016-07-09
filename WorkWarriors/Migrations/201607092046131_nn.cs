namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomeownerComfirmedBids", "Service_Number", c => c.Int(nullable: false));
            AddColumn("dbo.HomeownerComfirmedBids", "Confirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.ServiceRequests", "Confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "Confirmed");
            DropColumn("dbo.HomeownerComfirmedBids", "Confirmed");
            DropColumn("dbo.HomeownerComfirmedBids", "Service_Number");
        }
    }
}
