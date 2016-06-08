namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class service1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "PostedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ServiceRequests", "Bid", c => c.Double(nullable: false));
            AddColumn("dbo.ServiceRequests", "CompletionDeadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.ServiceRequests", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "Description");
            DropColumn("dbo.ServiceRequests", "CompletionDeadline");
            DropColumn("dbo.ServiceRequests", "Bid");
            DropColumn("dbo.ServiceRequests", "PostedDate");
        }
    }
}
