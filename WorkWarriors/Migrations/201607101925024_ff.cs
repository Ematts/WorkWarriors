namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "ContractorAcceptedBids_ID", c => c.Int());
            AddColumn("dbo.Files", "HomeownerComfirmedBids_ID", c => c.Int());
            AddColumn("dbo.Files", "ServiceRequest_ID", c => c.Int());
            CreateIndex("dbo.Files", "ContractorAcceptedBids_ID");
            CreateIndex("dbo.Files", "HomeownerComfirmedBids_ID");
            CreateIndex("dbo.Files", "ServiceRequest_ID");
            AddForeignKey("dbo.Files", "ContractorAcceptedBids_ID", "dbo.ContractorAcceptedBids", "ID");
            AddForeignKey("dbo.Files", "HomeownerComfirmedBids_ID", "dbo.HomeownerComfirmedBids", "ID");
            AddForeignKey("dbo.Files", "ServiceRequest_ID", "dbo.ServiceRequests", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropForeignKey("dbo.Files", "HomeownerComfirmedBids_ID", "dbo.HomeownerComfirmedBids");
            DropForeignKey("dbo.Files", "ContractorAcceptedBids_ID", "dbo.ContractorAcceptedBids");
            DropIndex("dbo.Files", new[] { "ServiceRequest_ID" });
            DropIndex("dbo.Files", new[] { "HomeownerComfirmedBids_ID" });
            DropIndex("dbo.Files", new[] { "ContractorAcceptedBids_ID" });
            DropColumn("dbo.Files", "ServiceRequest_ID");
            DropColumn("dbo.Files", "HomeownerComfirmedBids_ID");
            DropColumn("dbo.Files", "ContractorAcceptedBids_ID");
        }
    }
}
