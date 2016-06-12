namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractorAcceptedBids",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConUsername = c.String(),
                        HomeUsername = c.String(),
                        ConFirstName = c.String(),
                        HomeFirstname = c.String(),
                        ConLastName = c.String(),
                        HomeLastName = c.String(),
                        ConAddress = c.String(),
                        HomeAddress = c.String(),
                        ConCity = c.String(),
                        HomeCity = c.String(),
                        ConState = c.String(),
                        HomeState = c.String(),
                        ConZip = c.String(),
                        HomeZip = c.String(),
                        Conemail = c.String(),
                        Homeemail = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                        Bid = c.Double(nullable: false),
                        CompletionDeadline = c.DateTime(nullable: false),
                        Description = c.String(),
                        confirmed = c.Boolean(nullable: false),
                        ServiceRequest_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ServiceRequests", t => t.ServiceRequest_ID)
                .Index(t => t.ServiceRequest_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractorAcceptedBids", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.ContractorAcceptedBids", new[] { "ServiceRequest_ID" });
            DropTable("dbo.ContractorAcceptedBids");
        }
    }
}
