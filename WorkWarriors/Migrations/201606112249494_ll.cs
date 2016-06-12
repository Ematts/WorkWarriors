namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "ServiceRequest_ID", c => c.Int());
            CreateIndex("dbo.Contractors", "ServiceRequest_ID");
            AddForeignKey("dbo.Contractors", "ServiceRequest_ID", "dbo.ServiceRequests", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contractors", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.Contractors", new[] { "ServiceRequest_ID" });
            DropColumn("dbo.Contractors", "ServiceRequest_ID");
        }
    }
}
