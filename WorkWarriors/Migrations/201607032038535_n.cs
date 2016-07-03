namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BidAcceptances", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.BidAcceptances", new[] { "ServiceRequest_ID" });
            AlterColumn("dbo.ServiceRequests", "Username", c => c.String());
            AlterColumn("dbo.ServiceRequests", "FirstName", c => c.String());
            AlterColumn("dbo.ServiceRequests", "LastName", c => c.String());
            AlterColumn("dbo.ServiceRequests", "email", c => c.String(maxLength: 25));
            DropTable("dbo.BidAcceptances");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.ServiceRequests", "email", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.ServiceRequests", "LastName", c => c.String(maxLength: 25));
            AlterColumn("dbo.ServiceRequests", "FirstName", c => c.String(maxLength: 20));
            AlterColumn("dbo.ServiceRequests", "Username", c => c.String(maxLength: 15));
            CreateIndex("dbo.BidAcceptances", "ServiceRequest_ID");
            AddForeignKey("dbo.BidAcceptances", "ServiceRequest_ID", "dbo.ServiceRequests", "ID");
        }
    }
}
