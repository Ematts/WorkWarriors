namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contractors", "ServiceRequest_ID", "dbo.ServiceRequests");
            DropIndex("dbo.Contractors", new[] { "ServiceRequest_ID" });
            DropColumn("dbo.Contractors", "ServiceRequest_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contractors", "ServiceRequest_ID", c => c.Int());
            CreateIndex("dbo.Contractors", "ServiceRequest_ID");
            AddForeignKey("dbo.Contractors", "ServiceRequest_ID", "dbo.ServiceRequests", "ID");
        }
    }
}
