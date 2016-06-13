namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompletedBids",
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CompletedBids");
        }
    }
}
