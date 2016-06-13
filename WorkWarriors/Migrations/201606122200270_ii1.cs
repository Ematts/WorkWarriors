namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ii1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContractorAcceptedBids", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.ContractorAcceptedBids", new[] { "ServiceRequest_ID" });
            DropColumn("dbo.ContractorAcceptedBids", "ServiceRequest_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContractorAcceptedBids", "ServiceRequest_ID", c => c.Int());
            CreateIndex("dbo.ContractorAcceptedBids", "ServiceRequest_ID");
            AddForeignKey("dbo.ContractorAcceptedBids", "ServiceRequest_ID", "dbo.ServiceRequests", "ID");
        }
    }
}
