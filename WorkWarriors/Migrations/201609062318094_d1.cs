namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceRequests", "Address", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServiceRequests", "Address", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
