namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false, maxLength: 40),
                        City = c.String(nullable: false, maxLength: 40),
                        State = c.String(nullable: false, maxLength: 2),
                        Zip = c.String(nullable: false, maxLength: 5),
                        vacant = c.Boolean(nullable: false),
                        validated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Addresses");
        }
    }
}
