namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mm2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractorAcceptedBids", "expired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContractorAcceptedBids", "expired");
        }
    }
}
