namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class service : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "Username", c => c.String());
            AddColumn("dbo.ServiceRequests", "FirstName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.ServiceRequests", "LastName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.ServiceRequests", "Address", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.ServiceRequests", "City", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.ServiceRequests", "State", c => c.String(nullable: false, maxLength: 2));
            AddColumn("dbo.ServiceRequests", "Zip", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.ServiceRequests", "email", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "email");
            DropColumn("dbo.ServiceRequests", "Zip");
            DropColumn("dbo.ServiceRequests", "State");
            DropColumn("dbo.ServiceRequests", "City");
            DropColumn("dbo.ServiceRequests", "Address");
            DropColumn("dbo.ServiceRequests", "LastName");
            DropColumn("dbo.ServiceRequests", "FirstName");
            DropColumn("dbo.ServiceRequests", "Username");
        }
    }
}
