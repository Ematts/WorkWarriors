namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paths : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceRequestPaths",
                c => new
                    {
                        ServiceRequestPathId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        CompletedBids_ID = c.Int(),
                        ContractorAcceptedBids_ID = c.Int(),
                        HomeownerComfirmedBids_ID = c.Int(),
                        ServiceRequest_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ServiceRequestPathId)
                .ForeignKey("dbo.CompletedBids", t => t.CompletedBids_ID)
                .ForeignKey("dbo.ContractorAcceptedBids", t => t.ContractorAcceptedBids_ID)
                .ForeignKey("dbo.HomeownerComfirmedBids", t => t.HomeownerComfirmedBids_ID)
                .ForeignKey("dbo.ServiceRequests", t => t.ServiceRequest_ID)
                .Index(t => t.CompletedBids_ID)
                .Index(t => t.ContractorAcceptedBids_ID)
                .Index(t => t.HomeownerComfirmedBids_ID)
                .Index(t => t.ServiceRequest_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceRequestPaths", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropForeignKey("dbo.ServiceRequestPaths", "HomeownerComfirmedBids_ID", "dbo.HomeownerComfirmedBids");
            DropForeignKey("dbo.ServiceRequestPaths", "ContractorAcceptedBids_ID", "dbo.ContractorAcceptedBids");
            DropForeignKey("dbo.ServiceRequestPaths", "CompletedBids_ID", "dbo.CompletedBids");
            DropIndex("dbo.ServiceRequestPaths", new[] { "ServiceRequest_ID" });
            DropIndex("dbo.ServiceRequestPaths", new[] { "HomeownerComfirmedBids_ID" });
            DropIndex("dbo.ServiceRequestPaths", new[] { "ContractorAcceptedBids_ID" });
            DropIndex("dbo.ServiceRequestPaths", new[] { "CompletedBids_ID" });
            DropTable("dbo.ServiceRequestPaths");
        }
    }
}
