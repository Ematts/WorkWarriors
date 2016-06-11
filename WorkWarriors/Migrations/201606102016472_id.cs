namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Contractors", "UserId");
            AddForeignKey("dbo.Contractors", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contractors", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Contractors", new[] { "UserId" });
            DropColumn("dbo.Contractors", "UserId");
        }
    }
}
