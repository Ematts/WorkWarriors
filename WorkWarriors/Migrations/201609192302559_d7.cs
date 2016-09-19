namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Addresses", "UserId");
            AddForeignKey("dbo.Addresses", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropColumn("dbo.Addresses", "UserId");
        }
    }
}
