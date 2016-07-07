namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qq2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContractorAcceptedBids", "expired");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContractorAcceptedBids", "expired", c => c.Boolean(nullable: false));
        }
    }
}
