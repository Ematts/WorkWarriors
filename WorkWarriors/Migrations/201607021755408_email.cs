namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class email : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Homeowners", "email", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Homeowners", "email", c => c.String(nullable: false, maxLength: 25));
        }
    }
}
