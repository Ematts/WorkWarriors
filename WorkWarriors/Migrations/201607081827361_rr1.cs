namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rr1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompletedBids", "ContractorDue", c => c.Double(nullable: false));
            AddColumn("dbo.CompletedBids", "ContractorPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompletedBids", "ContractorPaid");
            DropColumn("dbo.CompletedBids", "ContractorDue");
        }
    }
}
