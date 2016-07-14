namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceRequestPaths", "ServiceRequestId", "dbo.ServiceRequests");
            DropIndex("dbo.ServiceRequestPaths", new[] { "ServiceRequestId" });
            RenameColumn(table: "dbo.ServiceRequestPaths", name: "ServiceRequestId", newName: "ServiceRequest_ID");
            AlterColumn("dbo.ServiceRequestPaths", "ServiceRequest_ID", c => c.Int());
            CreateIndex("dbo.ServiceRequestPaths", "ServiceRequest_ID");
            AddForeignKey("dbo.ServiceRequestPaths", "ServiceRequest_ID", "dbo.ServiceRequests", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceRequestPaths", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.ServiceRequestPaths", new[] { "ServiceRequest_ID" });
            AlterColumn("dbo.ServiceRequestPaths", "ServiceRequest_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ServiceRequestPaths", name: "ServiceRequest_ID", newName: "ServiceRequestId");
            CreateIndex("dbo.ServiceRequestPaths", "ServiceRequestId");
            AddForeignKey("dbo.ServiceRequestPaths", "ServiceRequestId", "dbo.ServiceRequests", "ID", cascadeDelete: true);
        }
    }
}
