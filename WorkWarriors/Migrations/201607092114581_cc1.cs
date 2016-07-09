namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cc1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompletedBids", "Service_Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompletedBids", "Service_Number");
        }
    }
}
