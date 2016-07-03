namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class o : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Homeowners", "Username", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Homeowners", "Username", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
