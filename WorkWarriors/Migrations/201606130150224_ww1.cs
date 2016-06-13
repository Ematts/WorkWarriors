namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ww1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractorAcceptedBids", "Invoice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContractorAcceptedBids", "Invoice");
        }
    }
}
