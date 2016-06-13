namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rr1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomeownerComfirmedBids",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConUsername = c.String(),
                        HomeUsername = c.String(),
                        ConFirstName = c.String(),
                        HomeFirstname = c.String(),
                        ConLastName = c.String(),
                        HomeLastName = c.String(),
                        ConAddress = c.String(),
                        HomeAddress = c.String(),
                        ConCity = c.String(),
                        HomeCity = c.String(),
                        ConState = c.String(),
                        HomeState = c.String(),
                        ConZip = c.String(),
                        HomeZip = c.String(),
                        ConEmail = c.String(),
                        HomeEmail = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                        Bid = c.Double(nullable: false),
                        CompletionDeadline = c.DateTime(nullable: false),
                        Description = c.String(),
                        Completed = c.Boolean(nullable: false),
                        Invoice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Homeowners", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Homeowners", "UserId");
            AddForeignKey("dbo.Homeowners", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Homeowners", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Homeowners", new[] { "UserId" });
            DropColumn("dbo.Homeowners", "UserId");
            DropTable("dbo.HomeownerComfirmedBids");
        }
    }
}
