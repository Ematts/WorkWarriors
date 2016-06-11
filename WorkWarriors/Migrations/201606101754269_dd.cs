namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ServiceRequests", "Contractor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceRequests", "Contractor", c => c.String());
        }
    }
}
