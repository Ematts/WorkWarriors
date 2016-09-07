namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceRequests", "Address", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServiceRequests", "Address", c => c.String(nullable: false, maxLength: 400));
        }
    }
}
