namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "Inactive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "Inactive");
        }
    }
}
