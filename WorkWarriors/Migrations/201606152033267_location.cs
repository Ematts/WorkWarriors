namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomeownerComfirmedBids", "JobLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomeownerComfirmedBids", "JobLocation");
        }
    }
}
