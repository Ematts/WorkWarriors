namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tt2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractorAcceptedBids", "ServiceNumber", c => c.Int(nullable: false));
            AddColumn("dbo.ServiceRequests", "ServiceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "ServiceNumber");
            DropColumn("dbo.ContractorAcceptedBids", "ServiceNumber");
        }
    }
}
