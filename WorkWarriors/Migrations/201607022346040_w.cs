namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class w : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contractors", "Username", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contractors", "Username", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
