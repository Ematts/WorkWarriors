namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceRequestPaths", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.ServiceRequestPaths", new[] { "ServiceRequest_ID" });
            RenameColumn(table: "dbo.ServiceRequestPaths", name: "ServiceRequest_ID", newName: "ServiceRequestId");
            AlterColumn("dbo.ServiceRequestPaths", "ServiceRequestId", c => c.Int(nullable: false));
            CreateIndex("dbo.ServiceRequestPaths", "ServiceRequestId");
            AddForeignKey("dbo.ServiceRequestPaths", "ServiceRequestId", "dbo.ServiceRequests", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceRequestPaths", "ServiceRequestId", "dbo.ServiceRequests");
            DropIndex("dbo.ServiceRequestPaths", new[] { "ServiceRequestId" });
            AlterColumn("dbo.ServiceRequestPaths", "ServiceRequestId", c => c.Int());
            RenameColumn(table: "dbo.ServiceRequestPaths", name: "ServiceRequestId", newName: "ServiceRequest_ID");
            CreateIndex("dbo.ServiceRequestPaths", "ServiceRequest_ID");
            AddForeignKey("dbo.ServiceRequestPaths", "ServiceRequest_ID", "dbo.ServiceRequests", "ID");
        }
    }
}
