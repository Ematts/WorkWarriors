namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HomeownerComfirmedBids", "Bid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CompletedBids", "Bid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CompletedBids", "ContractorDue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ContractorAcceptedBids", "Bid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ServiceRequests", "Bid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServiceRequests", "Bid", c => c.Double(nullable: false));
            AlterColumn("dbo.ContractorAcceptedBids", "Bid", c => c.Double(nullable: false));
            AlterColumn("dbo.CompletedBids", "ContractorDue", c => c.Double(nullable: false));
            AlterColumn("dbo.CompletedBids", "Bid", c => c.Double(nullable: false));
            AlterColumn("dbo.HomeownerComfirmedBids", "Bid", c => c.Double(nullable: false));
        }
    }
}
