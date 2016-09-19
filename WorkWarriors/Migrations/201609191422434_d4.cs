namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "validated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "validated");
        }
    }
}
