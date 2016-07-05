namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class we : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "expired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "expired");
        }
    }
}
