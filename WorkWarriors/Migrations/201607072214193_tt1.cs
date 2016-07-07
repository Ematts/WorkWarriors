namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tt1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractorAcceptedBids", "expired", c => c.Boolean(nullable: false));
            AddColumn("dbo.HomeownerComfirmedBids", "expired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomeownerComfirmedBids", "expired");
            DropColumn("dbo.ContractorAcceptedBids", "expired");
        }
    }
}
