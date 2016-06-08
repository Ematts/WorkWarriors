namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceRequests", "Username", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.ServiceRequests", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServiceRequests", "Description", c => c.String());
            AlterColumn("dbo.ServiceRequests", "Username", c => c.String());
        }
    }
}
