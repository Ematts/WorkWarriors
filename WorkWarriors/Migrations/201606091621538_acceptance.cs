namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acceptance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BidAcceptances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 40),
                        City = c.String(nullable: false, maxLength: 25),
                        State = c.String(nullable: false, maxLength: 2),
                        Zip = c.String(nullable: false, maxLength: 5),
                        PostedDate = c.DateTime(nullable: false),
                        Bid = c.Double(nullable: false),
                        CompletionDeadline = c.DateTime(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100),
                        ServiceRequest_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ServiceRequests", t => t.ServiceRequest_ID)
                .Index(t => t.ServiceRequest_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BidAcceptances", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.BidAcceptances", new[] { "ServiceRequest_ID" });
            DropTable("dbo.BidAcceptances");
        }
    }
}
